# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CabTap.Web/CabTap.Web.csproj", "CabTap.Web/"]
COPY ["CabTap.Services/CabTap.Services.csproj", "CabTap.Services/"]
COPY ["CabTap.Contracts/CabTap.Contracts.csproj", "CabTap.Contracts/"]
COPY ["CabTap.Core/CabTap.Core.csproj", "CabTap.Core/"]
COPY ["CabTap.Shared/CabTap.Shared.csproj", "CabTap.Shared/"]
COPY ["CabTap.Data/CabTap.Data.csproj", "CabTap.Data/"]
RUN dotnet restore "CabTap.Web/CabTap.Web.csproj"
COPY . .
WORKDIR "/src/CabTap.Web"
RUN dotnet build "./CabTap.Web.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "./CabTap.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CabTap.Web.dll"]
