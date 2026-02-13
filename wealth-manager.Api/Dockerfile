FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["wealth-manager.Api/wealth-manager.Api.csproj", "wealth-manager.Api/"]
RUN dotnet restore "wealth-manager.Api/wealth-manager.Api.csproj"
COPY . .
WORKDIR "/src/wealth-manager.Api"
RUN dotnet build "./wealth-manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./wealth-manager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wealth-manager.Api.dll"]
