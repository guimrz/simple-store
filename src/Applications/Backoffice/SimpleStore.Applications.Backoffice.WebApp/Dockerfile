#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Applications/Backoffice/SimpleStore.Applications.Backoffice.WebApp/SimpleStore.Applications.Backoffice.WebApp.csproj", "Applications/Backoffice/SimpleStore.Applications.Backoffice.WebApp/"]
RUN dotnet restore "Applications/Backoffice/SimpleStore.Applications.Backoffice.WebApp/SimpleStore.Applications.Backoffice.WebApp.csproj"
COPY . .
WORKDIR "/src/Applications/Backoffice/SimpleStore.Applications.Backoffice.WebApp"
RUN dotnet build "SimpleStore.Applications.Backoffice.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStore.Applications.Backoffice.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleStore.Applications.Backoffice.WebApp.dll"]