FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src
COPY ["ProductsMicroService.API/ProductsMicroService.API.csproj", "ProductsMicroService.API/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]

RUN dotnet restore "ProductsMicroService.API/ProductsMicroService.API.csproj" \
    --runtime linux-$TARGETARCH

COPY . .

WORKDIR "/src/ProductsMicroService.API"

FROM build AS publish

ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH

RUN dotnet publish "ProductsMicroService.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --runtime linux-$TARGETARCH \
    --no-restore \
    /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ProductsMicroService.API.dll"]