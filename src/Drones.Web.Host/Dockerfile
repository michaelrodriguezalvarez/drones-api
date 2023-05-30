FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Drones.Web.Host/Drones.Web.Host.csproj", "src/Drones.Web.Host/"]
COPY ["src/Drones.Web.Core/Drones.Web.Core.csproj", "src/Drones.Web.Core/"]
COPY ["src/Drones.Application/Drones.Application.csproj", "src/Drones.Application/"]
COPY ["src/Drones.Core/Drones.Core.csproj", "src/Drones.Core/"]
COPY ["src/Drones.EntityFrameworkCore/Drones.EntityFrameworkCore.csproj", "src/Drones.EntityFrameworkCore/"]
WORKDIR "/src/src/Drones.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Drones.Web.Host", "src/Drones.Web.Host"]
COPY ["src/Drones.Web.Core", "src/Drones.Web.Core"]
COPY ["src/Drones.Application", "src/Drones.Application"]
COPY ["src/Drones.Core", "src/Drones.Core"]
COPY ["src/Drones.EntityFrameworkCore", "src/Drones.EntityFrameworkCore"]
WORKDIR "/src/src/Drones.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Drones.Web.Host.dll"]
