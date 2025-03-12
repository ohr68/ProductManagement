FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ServiceOrder/ServiceOrder.WebApi/ServiceOrder.WebApi.csproj", "src/ServiceOrder/ServiceOrder.WebApi/"]
COPY ["src/Common/ProductManagement.Common/ProductManagement.Common.csproj", "src/Common/ProductManagement.Common/"]
COPY ["src/ServiceOrder/ServiceOrder.IoC/ServiceOrder.IoC.csproj", "src/ServiceOrder/ServiceOrder.IoC/"]
COPY ["src/ServiceOrder/ServiceOrder.Application/ServiceOrder.Application.csproj", "src/ServiceOrder/ServiceOrder.Application/"]
COPY ["src/ServiceOrder/ServiceOrder.Domain/ServiceOrder.Domain.csproj", "src/ServiceOrder/ServiceOrder.Domain/"]
COPY ["src/ServiceOrder/ServiceOrder.ORM/ServiceOrder.ORM.csproj", "src/ServiceOrder/ServiceOrder.ORM/"]
RUN dotnet restore "src/ServiceOrder/ServiceOrder.WebApi/ServiceOrder.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ServiceOrder/ServiceOrder.WebApi"
RUN dotnet build "ServiceOrder.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ServiceOrder.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceOrder.WebApi.dll"]
