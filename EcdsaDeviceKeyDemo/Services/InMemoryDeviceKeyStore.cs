using System.Collections.Concurrent;
using EcdsaDeviceKeyDemo.Models;

namespace EcdsaDeviceKeyDemo.Services;

/// <summary>
/// Demo-only store. Replace this with a database and a distributed, atomic
/// challenge-consumption operation before using the flow across instances.
/// </summary>
public sealed class InMemoryDeviceKeyStore : IDeviceKeyStore
{
    private readonly ConcurrentDictionary<string, DeviceKeyRecord> _devices = new(StringComparer.Ordinal);
    private readonly ConcurrentDictionary<string, ChallengeState> _challenges = new(StringComparer.Ordinal);
    private long _deviceVersion;

    public DeviceKeyRecord RegisterDevice(string deviceId, string publicKeySpkiBase64)
    {
        var record = new DeviceKeyRecord(
            deviceId,
            publicKeySpkiBase64,
            DateTimeOffset.UtcNow,
            Interlocked.Increment(ref _deviceVersion));

        _devices[deviceId] = record;
        return record;
    }

    public DeviceKeyRecord? GetDevice(string deviceId)
        => _devices.TryGetValue(deviceId, out var record) ? record : null;

    public void SaveChallenge(PendingChallenge challenge)
    {
        RemoveExpiredChallenges(DateTimeOffset.UtcNow);
        _challenges[challenge.ChallengeId] = new ChallengeState(challenge);
    }

    public PendingChallenge? GetChallenge(string challengeId)
        => _challenges.TryGetValue(challengeId, out var state) ? state.Challenge : null;

    public bool TryConsumeChallenge(string challengeId, DateTimeOffset nowUtc)
    {
        if (!_challenges.TryGetValue(challengeId, out var state))
        {
            return false;
        }

        lock (state.SyncRoot)
        {
            if (state.Consumed || nowUtc > state.Challenge.ExpiresAtUtc)
            {
                return false;
            }

            state.Consumed = true;
            return true;
        }
    }

    private void RemoveExpiredChallenges(DateTimeOffset nowUtc)
    {
        foreach (var pair in _challenges)
        {
            if (pair.Value.Challenge.ExpiresAtUtc < nowUtc)
            {
                _challenges.TryRemove(pair.Key, out _);
            }
        }
    }

    private sealed class ChallengeState(PendingChallenge challenge)
    {
        public PendingChallenge Challenge { get; } = challenge;
        public object SyncRoot { get; } = new();
        public bool Consumed { get; set; }
    }
}
