# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy just the csproj and restore first (Docker caches this layer)
COPY CoolApp/CoolApp.csproj CoolApp/
RUN dotnet restore CoolApp/CoolApp.csproj

# Copy the rest of the app and publish a Release build
COPY CoolApp/ CoolApp/
RUN dotnet publish CoolApp/CoolApp.csproj -c Release -o /app/publish

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# App listens on port 8080 inside the container
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ARG GIT_COMMIT=local
ENV GIT_COMMIT=$GIT_COMMIT

ENTRYPOINT ["dotnet", "CoolApp.dll"]