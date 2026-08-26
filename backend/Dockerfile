# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["WanderKiwi.sln", "./"]
COPY ["src/WanderKiwi.Api/WanderKiwi.Api.csproj", "src/WanderKiwi.Api/"]
COPY ["src/WanderKiwi.Infrastructure/WanderKiwi.Infrastructure.csproj", "src/WanderKiwi.Infrastructure/"]
# (Include any other project files you have in your solution here)

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .
WORKDIR "/src/WanderKiwi.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WanderKiwi.Api.dll"]
