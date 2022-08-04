#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["JogoDaVelha.Api/JogoDaVelha.Api.csproj", "JogoDaVelha.Api/"]
COPY ["JogoDaVelha.Biblioteca/JogoDaVelha.Biblioteca.csproj", "JogoDaVelha.Biblioteca/"]
COPY ["JogoDaVelha.Repositorio/JogoDaVelha.Repositorio.csproj", "JogoDaVelha.Repositorio/"]
COPY ["JogoDaVelha.Dominio/JogoDaVelha.Dominio.csproj", "JogoDaVelha.Dominio/"]
RUN dotnet restore "JogoDaVelha.Api/JogoDaVelha.Api.csproj"
COPY . .
WORKDIR "/src/JogoDaVelha.Api"
RUN dotnet build "JogoDaVelha.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JogoDaVelha.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JogoDaVelha.Api.dll"]