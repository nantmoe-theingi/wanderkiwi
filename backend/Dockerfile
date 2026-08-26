# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file
COPY ["WanderKiwi.sln", "./"]

# Copy all project files maintaining their relative paths from backend folder
COPY ["src/WanderKiwi.API/WanderKiwi.API.csproj", "src/WanderKiwi.API/"]
COPY ["src/WanderKiwi.Application/WanderKiwi.Application.csproj", "src/WanderKiwi.Application/"]
COPY ["src/WanderKiwi.Domain/WanderKiwi.Domain.csproj", "src/WanderKiwi.Domain/"]
COPY ["src/WanderKiwi.Infrastructure/WanderKiwi.Infrastructure.csproj", "src/WanderKiwi.Infrastructure/"]
COPY ["src/WanderKiwi.Shared/WanderKiwi.Shared.csproj", "src/WanderKiwi.Shared/"]
COPY ["tests/WanderKiwi.API.Tests/WanderKiwi.API.Tests.csproj", "tests/WanderKiwi.API.Tests/"]
COPY ["tests/WanderKiwi.Application.Tests/WanderKiwi.Application.Tests.csproj", "tests/WanderKiwi.Application.Tests/"]
COPY ["tests/WanderKiwi.Domain.Tests/WanderKiwi.Domain.Tests.csproj", "tests/WanderKiwi.Domain.Tests/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .
RUN dotnet publish src/WanderKiwi.API/WanderKiwi.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WanderKiwi.API.dll"]
