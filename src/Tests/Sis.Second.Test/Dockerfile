FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Tests/Sis.Second.Test/Sis.Second.Test.csproj", "src/Tests/Sis.Second.Test/"]
COPY ["src/SIS.Second/SIS.Second.csproj", "src/SIS.Second/"]
COPY ["src/SIS.Domain.Model/SIS.Domain.Model.csproj", "src/SIS.Domain.Model/"]
COPY ["src/SIS.Domain.Contract/SIS.Domain.Contract/SIS.Domain.Contract.csproj", "src/SIS.Domain.Contract/SIS.Domain.Contract/"]
COPY . .
WORKDIR "/src/src/Tests/Sis.Second.Test"
ENTRYPOINT ["dotnet", "test"]