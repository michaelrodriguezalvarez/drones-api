# Drones

## Instructions

## Install Postgres

<https://www.postgresql.org/download/>

## Install dotnet sdk 7.0

<https://dotnet.microsoft.com/en-us/download/dotnet/7.0/>

## Restore NuGet packages

Run `dotnet restore`

## Build the solution

Run `dotnet build "Drones.sln" -c Release -o /app/build`

## Public Web project

Run `dotnet publish "src/Drones.Web.Host/Drones.Web.Host.csproj" -c Debug -o /app/publish --no-restore`

## Configure appsettings.json file with Postgres configuration

Change the follow line:

`"Default": "User ID=postgres;Database=DronesDb;Password=SecretPassword;Host=localhost;Port=5432;Pooling=true;"`

## Install dotnet tools

Run `dotnet tool install --global dotnet-ef`

## Create database and run migrations

Run `dotnet-ef database update --project src/Drones.EntityFrameworkCore/Drones.EntityFrameworkCore.csproj --configuration Debug --verbose`

## Install dotnet aspnet 7.0

<https://dotnet.microsoft.com/en-us/download/dotnet/7.0/>

## Run application

Run `dotnet "/app/publish/Drones.Web.Host.dll"`
