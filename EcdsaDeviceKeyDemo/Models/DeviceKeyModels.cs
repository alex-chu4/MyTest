namespace EcdsaDeviceKeyDemo.Models;

public sealed record RegisterDeviceRequest(string? DeviceId, string? PublicKey);

public sealed record RegisterDeviceResponse(
    bool Ok,
    string Message,
    string? DeviceId = null,
    DateTimeOffset? RegisteredAt = null);

public sealed record ChallengeResponse(
    bool Ok,
    string Message,
    string? ChallengeId = null,
    string? DeviceId = null,
    string? Nonce = null,
    DateTimeOffset? IssuedAt = null,
    DateTimeOffset? ExpiresAt = null,
    string? MessageToSign = null);

public sealed record VerifyDeviceRequest(
    string? DeviceId,
    string? ChallengeId,
    string? Message,
    string? Signature);

public sealed record VerifyResponse(
    bool Ok,
    string Message,
    bool? SignatureValid = null,
    DateTimeOffset? VerifiedAt = null);

public sealed record DeviceKeyRecord(
    string DeviceId,
    string PublicKeySpkiBase64,
    DateTimeOffset RegisteredAtUtc,
    long Version);

public sealed record PendingChallenge(
    string ChallengeId,
    string DeviceId,
    string Nonce,
    DateTimeOffset IssuedAtUtc,
    DateTimeOffset ExpiresAtUtc,
    string MessageToSign,
    long RegistrationVersion);
