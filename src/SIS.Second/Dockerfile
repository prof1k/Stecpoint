#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SIS.Second/SIS.Second.csproj", "src/SIS.Second/"]
COPY ["src/SIS.Domain.Model/SIS.Domain.Model.csproj", "src/SIS.Domain.Model/"]
COPY ["src/SIS.Domain.Contract/SIS.Domain.Contract/SIS.Domain.Contract.csproj", "src/SIS.Domain.Contract/SIS.Domain.Contract/"]
RUN dotnet restore "src/SIS.Second/SIS.Second.csproj"
COPY . .
WORKDIR "/src/src/SIS.Second"
RUN dotnet build "SIS.Second.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SIS.Second.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SIS.Second.dll"]