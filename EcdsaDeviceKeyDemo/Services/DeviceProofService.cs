using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using EcdsaDeviceKeyDemo.Models;

namespace EcdsaDeviceKeyDemo.Services;

public sealed class DeviceProofService(IDeviceKeyStore store)
{
    private const string P256Oid = "1.2.840.10045.3.1.7";
    private static readonly TimeSpan ChallengeLifetime = TimeSpan.FromMinutes(2);

    public void Register(RegisterDeviceRequest? request, out RegisterDeviceResponse response, out int statusCode)
    {
        if (request is null || !IsValidDeviceId(request.DeviceId))
        {
            response = new(false, "DeviceId 不可為空，且長度不可超過 128 字元。");
            statusCode = StatusCodes.Status400BadRequest;
            return;
        }

        if (!TryValidateP256Spki(request.PublicKey, out var normalizedPublicKey))
        {
            response = new(false, "Public Key 不是有效的 ECDSA P-256 SubjectPublicKeyInfo Base64。");
            statusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var record = store.RegisterDevice(request.DeviceId!, normalizedPublicKey);
        response = new(true, "Server 已記住 ECDSA P-256 Public Key。", record.DeviceId, record.RegisteredAtUtc);
        statusCode = StatusCodes.Status200OK;
    }

    public void CreateChallenge(string? deviceId, out ChallengeResponse response, out int statusCode)
    {
        if (!IsValidDeviceId(deviceId))
        {
            response = new(false, "DeviceId 不可為空，且長度不可超過 128 字元。");
            statusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var device = store.GetDevice(deviceId!);
        if (device is null)
        {
            response = new(false, "找不到此 DeviceId，請先註冊 Public Key。");
            statusCode = StatusCodes.Status404NotFound;
            return;
        }

        var issuedAt = DateTimeOffset.UtcNow;
        var expiresAt = issuedAt.Add(ChallengeLifetime);
        var challengeId = Guid.NewGuid().ToString("N");
        var nonce = CreateNonce();
        var messageToSign = BuildMessageToSign(challengeId, deviceId!, nonce, issuedAt, expiresAt);

        store.SaveChallenge(new PendingChallenge(
            challengeId,
            deviceId!,
            nonce,
            issuedAt,
            expiresAt,
            messageToSign,
            device.Version));

        response = new(true, "Challenge 已建立，請使用瀏覽器 Private Key 簽章。", challengeId, deviceId, nonce, issuedAt, expiresAt, messageToSign);
        statusCode = StatusCodes.Status200OK;
    }

    public void Verify(VerifyDeviceRequest? request, out VerifyResponse response, out int statusCode)
    {
        if (request is null ||
            !IsValidDeviceId(request.DeviceId) ||
            string.IsNullOrWhiteSpace(request.ChallengeId) ||
            string.IsNullOrWhiteSpace(request.Message) ||
            string.IsNullOrWhiteSpace(request.Signature))
        {
            response = new(false, "DeviceId、ChallengeId、Message、Signature 都是必要欄位。", false);
            statusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var device = store.GetDevice(request.DeviceId!);
        var challenge = store.GetChallenge(request.ChallengeId!);
        if (device is null || challenge is null)
        {
            response = new(false, "Device 或 Challenge 不存在。", false);
            statusCode = StatusCodes.Status404NotFound;
            return;
        }

        var now = DateTimeOffset.UtcNow;
        if (now > challenge.ExpiresAtUtc)
        {
            response = new(false, "Challenge 已過期，請重新取得。", false);
            statusCode = StatusCodes.Status410Gone;
            return;
        }

        if (!string.Equals(challenge.DeviceId, request.DeviceId, StringComparison.Ordinal) ||
            device.Version != challenge.RegistrationVersion ||
            !string.Equals(challenge.MessageToSign, request.Message, StringComparison.Ordinal))
        {
            response = new(false, "Challenge、DeviceId 或待簽訊息不相符。", false);
            statusCode = StatusCodes.Status400BadRequest;
            return;
        }

        if (!TryDecodeBase64(request.Signature!, out var signature) || signature.Length > 512)
        {
            response = new(false, "Signature 不是有效的 Base64 簽章。", false);
            statusCode = StatusCodes.Status400BadRequest;
            return;
        }

        bool valid;
        try
        {
            using var ecdsa = ECDsa.Create();
            var publicKey = Convert.FromBase64String(device.PublicKeySpkiBase64);
            ecdsa.ImportSubjectPublicKeyInfo(publicKey, out var bytesRead);
            if (bytesRead != publicKey.Length)
            {
                response = new(false, "Public Key 格式不完整。", false);
                statusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var data = Encoding.UTF8.GetBytes(request.Message!);
            valid = TryVerify(ecdsa, data, signature);
        }
        catch (CryptographicException)
        {
            valid = false;
        }
        catch (FormatException)
        {
            valid = false;
        }

        if (!valid)
        {
            response = new(false, "Signature 驗證失敗。", false);
            statusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        // Verification can be performed concurrently. Only one request may consume a valid challenge.
        if (!store.TryConsumeChallenge(challenge.ChallengeId, now))
        {
            response = new(false, "Challenge 已使用或已過期，不能 Replay。", false);
            statusCode = StatusCodes.Status409Conflict;
            return;
        }

        response = new(true, "Signature 驗證成功，Challenge 已標記為一次性使用。", true, now);
        statusCode = StatusCodes.Status200OK;
    }

    public static string BuildMessageToSign(
        string challengeId,
        string deviceId,
        string nonce,
        DateTimeOffset issuedAt,
        DateTimeOffset expiresAt)
    {
        return string.Join('\n',
            "ECDSA-DEVICE-PROOF/v1",
            $"challengeId:{challengeId}",
            $"deviceId:{deviceId}",
            $"nonce:{nonce}",
            $"issuedAt:{issuedAt.ToString("O", CultureInfo.InvariantCulture)}",
            $"expiresAt:{expiresAt.ToString("O", CultureInfo.InvariantCulture)}");
    }

    private static bool TryValidateP256Spki(string? value, out string normalized)
    {
        normalized = string.Empty;
        if (string.IsNullOrWhiteSpace(value) || value.Length > 8192 || !TryDecodeBase64(value, out var bytes))
        {
            return false;
        }

        try
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportSubjectPublicKeyInfo(bytes, out var bytesRead);
            if (bytesRead != bytes.Length)
            {
                return false;
            }

            var parameters = ecdsa.ExportParameters(false);
            var curveOid = parameters.Curve.Oid.Value;
            if (!string.Equals(curveOid, P256Oid, StringComparison.Ordinal))
            {
                return false;
            }

            normalized = Convert.ToBase64String(bytes);
            return true;
        }
        catch (CryptographicException)
        {
            return false;
        }
        catch (PlatformNotSupportedException)
        {
            return false;
        }
    }

    private static bool TryVerify(ECDsa ecdsa, byte[] data, byte[] signature)
    {
        try
        {
            return ecdsa.VerifyData(
                       data,
                       signature,
                       HashAlgorithmName.SHA256,
                       DSASignatureFormat.IeeeP1363FixedFieldConcatenation) ||
                   ecdsa.VerifyData(
                       data,
                       signature,
                       HashAlgorithmName.SHA256,
                       DSASignatureFormat.Rfc3279DerSequence);
        }
        catch (CryptographicException)
        {
            return false;
        }
    }

    private static bool TryDecodeBase64(string value, out byte[] bytes)
    {
        try
        {
            bytes = Convert.FromBase64String(value);
            return bytes.Length > 0;
        }
        catch (FormatException)
        {
            bytes = [];
            return false;
        }
    }

    private static string CreateNonce()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');

    private static bool IsValidDeviceId(string? value)
        => !string.IsNullOrWhiteSpace(value) &&
           value.Length <= 128 &&
           value.All(character => !char.IsControl(character));
}
