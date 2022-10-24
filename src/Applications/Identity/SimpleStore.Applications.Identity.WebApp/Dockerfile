#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Applications/Identity/SimpleStore.Applications.Identity.WebApp/SimpleStore.Applications.Identity.WebApp.csproj", "Applications/Identity/SimpleStore.Applications.Identity.WebApp/"]
COPY ["Core/SimpleStore.Core.EntityFrameworkCore.SqlServer/SimpleStore.Core.EntityFrameworkCore.SqlServer.csproj", "Core/SimpleStore.Core.EntityFrameworkCore.SqlServer/"]
COPY ["Core/SimpleStore.Core.EntityFrameworkCore/SimpleStore.Core.EntityFrameworkCore.csproj", "Core/SimpleStore.Core.EntityFrameworkCore/"]
COPY ["Core/SimpleStore.Core/SimpleStore.Core.csproj", "Core/SimpleStore.Core/"]
RUN dotnet restore "Applications/Identity/SimpleStore.Applications.Identity.WebApp/SimpleStore.Applications.Identity.WebApp.csproj"
COPY . .
WORKDIR "/src/Applications/Identity/SimpleStore.Applications.Identity.WebApp"
RUN dotnet build "SimpleStore.Applications.Identity.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStore.Applications.Identity.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SimpleStore.Applications.Identity.WebApp.dll"]