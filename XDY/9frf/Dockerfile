#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["/9frf.csproj", "9frf/"]
RUN dotnet restore "9frf/9frf.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "9frf.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "9frf.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "9frf.dll"]