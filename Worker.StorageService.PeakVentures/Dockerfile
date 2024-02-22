#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Worker.StorageService.PeakVentures/01-Worker.StorageService.PeakVentures.csproj", "Worker.StorageService.PeakVentures/"]
COPY ["Core.StorageSevice.PeakVentures/02-Core.StorageSevice.PeakVentures.csproj", "Core.StorageSevice.PeakVentures/"]
RUN dotnet restore "Worker.StorageService.PeakVentures/01-Worker.StorageService.PeakVentures.csproj"
COPY . .
WORKDIR "/src/Worker.StorageService.PeakVentures"
RUN dotnet build "01-Worker.StorageService.PeakVentures.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "01-Worker.StorageService.PeakVentures.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "01-Worker.StorageService.PeakVentures.dll"]