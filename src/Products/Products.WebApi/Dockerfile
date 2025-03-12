FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Products/Products.WebApi/Products.WebApi.csproj", "src/Products/Products.WebApi/"]
COPY ["src/Common/ProductManagement.Common/ProductManagement.Common.csproj", "src/Common/ProductManagement.Common/"]
COPY ["src/Products/Products.IoC/Products.IoC.csproj", "src/Products/Products.IoC/"]
COPY ["src/Products/Products.Application/Products.Application.csproj", "src/Products/Products.Application/"]
COPY ["src/Products/Products.Domain/Products.Domain.csproj", "src/Products/Products.Domain/"]
COPY ["src/Products/Products.ORM/Products.ORM.csproj", "src/Products/Products.ORM/"]
RUN dotnet restore "src/Products/Products.WebApi/Products.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Products/Products.WebApi"
RUN dotnet build "Products.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Products.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.WebApi.dll"]
