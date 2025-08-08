# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies (better cache)
COPY *.sln .
COPY *.csproj ./
RUN dotnet restore

# Copy all source files
COPY . .

# Publish app (no app host for smaller image)
RUN dotnet publish -c Release -o out /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published app and SQLite DB
COPY --from=build /app/out .
COPY --from=build /app/app.db .

# Expose port
EXPOSE 80

# Production environment
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80
# Start app
ENTRYPOINT ["dotnet", "RUM.dll"]
