# 此階段用於以快速模式從 VS 執行時
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app

# 建立 uploads，並設定 ASP.NET Core 使用者擁有權
RUN mkdir -p /app/uploads \
    && chmod 777 /app/uploads \
    && echo "===== UPLOAD PERMISSION =====" \
    && ls -ld /app/uploads \
    && id


USER $APP_UID

EXPOSE 8080
EXPOSE 8081


# 建置階段
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["ImageChat3-1.csproj", "."]

RUN dotnet restore "ImageChat3-1.csproj"

COPY . .

RUN dotnet build "ImageChat3-1.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build


# 發布階段
FROM build AS publish

ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "ImageChat3-1.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false


# 最終執行階段
FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ImageChat3-1.dll"]