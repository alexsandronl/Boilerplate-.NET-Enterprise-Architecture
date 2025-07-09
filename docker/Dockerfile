# Dockerfile para API .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/Api/Api.csproj ./Api/
COPY src/Application/Application.csproj ./Application/
COPY src/Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY src/Domain/Domain.csproj ./Domain/
COPY src/Shared/Shared.csproj ./Shared/
RUN dotnet restore "Api/Api.csproj"
COPY src/ ./
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"] 