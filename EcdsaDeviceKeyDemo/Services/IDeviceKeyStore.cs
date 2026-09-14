using EcdsaDeviceKeyDemo.Models;

namespace EcdsaDeviceKeyDemo.Services;

public interface IDeviceKeyStore
{
    DeviceKeyRecord RegisterDevice(string deviceId, string publicKeySpkiBase64);

    DeviceKeyRecord? GetDevice(string deviceId);

    void SaveChallenge(PendingChallenge challenge);

    PendingChallenge? GetChallenge(string challengeId);

    bool TryConsumeChallenge(string challengeId, DateTimeOffset nowUtc);
}
