#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN echo '103.233.100.16 see.labs.telkomuniversity.ac.id' >> /etc/hosts

WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SealabAPI.csproj", "./"]
RUN dotnet restore "SealabAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SealabAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SealabAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SealabAPI.dll"]
