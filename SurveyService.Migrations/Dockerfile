﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SurveyService.Migrations/SurveyService.Migrations.csproj", "SurveyService.Migrations/"]
RUN dotnet restore "SurveyService.Migrations/SurveyService.Migrations.csproj"
COPY . .
WORKDIR "/src/SurveyService.Migrations"
RUN dotnet build "SurveyService.Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SurveyService.Migrations.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./SurveyService.Migrations/Migrations/*.sql ./Migrations/
ENTRYPOINT ["dotnet", "SurveyService.Migrations.dll"]
