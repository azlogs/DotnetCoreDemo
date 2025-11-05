# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlogEngine/BlogEngine.API.csproj", "BlogEngine/"]
COPY ["BlogEngine.DataModels/BlogEngine.DataModels.csproj", "BlogEngine.DataModels/"]
COPY ["BlogEngine.ViewModels/BlogEngine.ViewModels.csproj", "BlogEngine.ViewModels/"]
COPY ["BlogEngine.Services/BlogEngine.Services.csproj", "BlogEngine.Services/"]
COPY ["BlogEngine.DataRepositories/BlogEngine.DataRepositories.csproj", "BlogEngine.DataRepositories/"]
COPY ["BlogEngine.Utils/BlogEngine.Utils.csproj", "BlogEngine.Utils/"]
RUN dotnet restore "./BlogEngine/BlogEngine.API.csproj"
COPY . .
WORKDIR "/src/BlogEngine"
RUN dotnet build "./BlogEngine.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlogEngine.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogEngine.API.dll"]
