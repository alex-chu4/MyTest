using EcdsaDeviceKeyDemo.Models;
using EcdsaDeviceKeyDemo.Services;

namespace EcdsaDeviceKeyDemo.Api;

public static class DeviceKeyEndpoints
{
    public static IEndpointRouteBuilder MapDeviceKeyEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/device").WithTags("Device key");

        group.MapPost("/register", (RegisterDeviceRequest? request, DeviceProofService service) =>
        {
            service.Register(request, out var response, out var statusCode);
            return Results.Json(response, statusCode: statusCode);
        });

        group.MapGet("/challenge", (string? deviceId, DeviceProofService service) =>
        {
            service.CreateChallenge(deviceId, out var response, out var statusCode);
            return Results.Json(response, statusCode: statusCode);
        });

        group.MapPost("/verify", (VerifyDeviceRequest? request, DeviceProofService service) =>
        {
            service.Verify(request, out var response, out var statusCode);
            return Results.Json(response, statusCode: statusCode);
        });

        return endpoints;
    }
}
