# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["WanderKiwi.sln", "./"]
COPY ["src/WanderKiwi.API/WanderKiwi.API.csproj", "src/WanderKiwi.API/"]
COPY ["src/WanderKiwi.Infrastructure/WanderKiwi.Infrastructure.csproj", "src/WanderKiwi.Infrastructure/"]
# (Include other project files here if you have them, e.g., Application/Domain)

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .
WORKDIR "/src/src/WanderKiwi.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WanderKiwi.API.dll"]
