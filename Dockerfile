FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY evaluacion20262.csproj ./
RUN dotnet restore evaluacion20262.csproj
COPY . .
RUN dotnet publish evaluacion20262.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
ENV DOTNET_RUNNING_IN_CONTAINER=true
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 CMD wget -qO- http://localhost:8080/ || exit 1
ENTRYPOINT ["dotnet", "evaluacion20262.dll"]
