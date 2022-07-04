#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Testcase.CSV.API/Testcase.CSV.API.csproj", "Testcase.CSV.API/"]
COPY ["Testcase.CSV.Application/Testcase.CSV.Application.csproj", "Testcase.CSV.Application/"]
COPY ["Testcase.CSV.Domain/Testcase.CSV.Domain.csproj", "Testcase.CSV.Domain/"]
COPY ["TestCase.ICsvInfrastructure/TestCase.ICsvInfrastructure.csproj", "TestCase.ICsvInfrastructure/"]
RUN dotnet restore "Testcase.CSV.API/Testcase.CSV.API.csproj"
COPY . .
WORKDIR "/src/Testcase.CSV.API"
RUN dotnet build "Testcase.CSV.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Testcase.CSV.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testcase.CSV.API.dll"]
