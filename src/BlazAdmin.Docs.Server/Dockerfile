#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BlazAdmin.Docs.Server/BlazAdmin.Docs.Server.csproj", "src/BlazAdmin.Docs.Server/"]
RUN dotnet restore "src/BlazAdmin.Docs.Server/BlazAdmin.Docs.Server.csproj"
COPY . .
WORKDIR "/src/src/BlazAdmin.Docs.Server"
RUN dotnet build "BlazAdmin.Docs.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazAdmin.Docs.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazAdmin.Docs.Server.dll"]