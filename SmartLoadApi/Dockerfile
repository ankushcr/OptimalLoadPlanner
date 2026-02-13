# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore
COPY SmartLoadApi/SmartLoadApi.csproj SmartLoadApi/
RUN dotnet restore SmartLoadApi/SmartLoadApi.csproj

# Copy everything and publish
COPY . .
WORKDIR /src/SmartLoadApi
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartLoadApi.dll"]
