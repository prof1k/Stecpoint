FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Tests/Sis.First.Test/Sis.First.Test.csproj", "src/Tests/Sis.First.Test/"]
COPY ["src/SIS.First/SIS.First.csproj", "src/SIS.First/"]
COPY ["src/SIS.Domain.Model/SIS.Domain.Model.csproj", "src/SIS.Domain.Model/"]
COPY ["src/SIS.Domain.Contract/SIS.Domain.Contract/SIS.Domain.Contract.csproj", "src/SIS.Domain.Contract/SIS.Domain.Contract/"]
COPY . .
WORKDIR "/src/src/Tests/Sis.First.Test"
ENTRYPOINT ["dotnet", "test"]