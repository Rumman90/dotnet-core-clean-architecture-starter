# Simple multi-stage Dockerfile for .NET Core Web API

# 1. Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 2. Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/Services/Services.csproj", "src/Services/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Data/Data.csproj", "src/Data/"]
RUN dotnet restore "src/WebApi/WebApi.csproj"

COPY . .
WORKDIR "/src/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

# 3. Publish
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

# 4. Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WebApi.dll"]
