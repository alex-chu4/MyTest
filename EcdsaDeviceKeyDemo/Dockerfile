# =========================
# Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app

# Render 使用 PORT 環境變數
ENV ASPNETCORE_HTTP_PORTS=10000

EXPOSE 10000


# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["EcdsaDeviceKeyDemo.csproj", "./"]

RUN dotnet restore "EcdsaDeviceKeyDemo.csproj"

COPY . .

RUN dotnet build "EcdsaDeviceKeyDemo.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build \
    --no-restore


# =========================
# Publish
# =========================
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "EcdsaDeviceKeyDemo.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false \
    --no-restore


# =========================
# Final
# =========================
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EcdsaDeviceKeyDemo.dll"]