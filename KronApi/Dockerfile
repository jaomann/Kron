FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KronApi/KronApi.csproj", "KronApi/"]
RUN dotnet restore "KronApi/KronApi.csproj"
COPY . .
WORKDIR "/src/KronApi"
RUN dotnet build "KronApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KronApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KronApi.dll"]

# Healthcheck configuration
HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
    CMD curl -f http://localhost/health || exit 1
