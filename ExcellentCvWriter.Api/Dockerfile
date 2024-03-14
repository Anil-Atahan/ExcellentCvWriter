FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExcellentCvWriter.Api/ExcellentCvWriter.Api.csproj", "ExcellentCvWriter.Api/"]
COPY ["Modules.Notifications.Infrastructure/Modules.Notifications.Infrastructure.csproj", "Modules.Notifications.Infrastructure/"]
COPY ["Modules.Users.Infrastructure/Modules.Users.Infrastructure.csproj", "Modules.Users.Infrastructure/"]
COPY ["Modules.Users.Endpoints/Modules.Users.Endpoints.csproj", "Modules.Users.Endpoints/"]
COPY ["Modules.Users.Application/Modules.Users.Application.csproj", "Modules.Users.Application/"]
COPY ["Modules.Users.Domain/Modules.Users.Domain.csproj", "Modules.Users.Domain/"]
COPY ["ExcellentCvWriter.SharedKernel/ExcellentCvWriter.SharedKernel.csproj", "ExcellentCvWriter.SharedKernel/"]
COPY ["Modules.Users.IntegrationEvents/Modules.Users.IntegrationEvents.csproj", "Modules.Users.IntegrationEvents/"]
COPY ["Modules.Users.Persistence/Modules.Users.Persistence.csproj", "Modules.Users.Persistence/"]
RUN dotnet restore "ExcellentCvWriter.Api/ExcellentCvWriter.Api.csproj"
COPY . .
WORKDIR "/src/ExcellentCvWriter.Api"
RUN dotnet build "ExcellentCvWriter.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExcellentCvWriter.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExcellentCvWriter.Api.dll"]
