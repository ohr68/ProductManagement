FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Auth/Auth.WebApi/Auth.WebApi.csproj", "src/Auth/Auth.WebApi/"]
COPY ["src/Auth/Auth.IoC/Auth.IoC.csproj", "src/Auth/Auth.IoC/"]
COPY ["src/Auth/Auth.Application/Auth.Application.csproj", "src/Auth/Auth.Application/"]
COPY ["src/Auth/Auth.Domain/Auth.Domain.csproj", "src/Auth/Auth.Domain/"]
COPY ["src/Auth/Auth.ORM/Auth.ORM.csproj", "src/Auth/Auth.ORM/"]
COPY ["src/Common/ProductManagement.Common/ProductManagement.Common.csproj", "src/Common/ProductManagement.Common/"]
COPY ["src/Auth/Auth.Jwt/Auth.Jwt.csproj", "src/Auth/Auth.Jwt/"]
RUN dotnet restore "src/Auth/Auth.WebApi/Auth.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Auth/Auth.WebApi"
RUN dotnet build "Auth.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Auth.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.WebApi.dll"]
