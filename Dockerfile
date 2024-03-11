#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
#Libraries
COPY ["RL.DataAccess/RL.DataAccess.csproj", "RL.DataAccess/"]
COPY ["RL.Entity/RL.Entity.csproj", "RL.Entity/"]
COPY ["RL.Share/RL.Share.csproj", "RL.Share/"]
COPY ["RL.Utility/RL.Utility.csproj", "RL.Utility/"]
COPY ["RL.Services/RL.Services.csproj", "RL.Services/"]
COPY ["RL.API/RL.API.csproj", "RL.API/"]
RUN dotnet restore "./RL.API/RL.API.csproj"
COPY . .
WORKDIR "/app/RL.API"
RUN dotnet build "./RL.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RL.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RL.API.dll"]