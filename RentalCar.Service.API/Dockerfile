FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentalCar.Service.API/RentalCar.Service.API.csproj", "RentalCar.Service.API/"]
COPY ["RentalCar.Service.Application/RentalCar.Service.Application.csproj", "RentalCar.Service.Application/"]
COPY ["RentalCar.Service.Core/RentalCar.Service.Core.csproj", "RentalCar.Service.Core/"]
COPY ["RentalCar.Service.Infrastructure/RentalCar.Service.Infrastructure.csproj", "RentalCar.Service.Infrastructure/"]
RUN dotnet restore "RentalCar.Service.API/RentalCar.Service.API.csproj"
COPY . .
WORKDIR "/src/RentalCar.Service.API"
RUN dotnet build "RentalCar.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RentalCar.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalCar.Service.API.dll"]
