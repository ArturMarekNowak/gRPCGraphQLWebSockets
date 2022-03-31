FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80
RUN apt-get update
RUN apt-get upgrade
RUN apt-get install -y sqlite3 libsqlite3-dev

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "gRPCGraphQLWebSockets.dll"]