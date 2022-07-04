#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Testcase.API/Testcase.API.csproj", "Testcase.API/"]
COPY ["Testcase.Infrastructure/Testcase.Infrastructure.csproj", "Testcase.Infrastructure/"]
COPY ["Testcase.User.Domain/Testcase.User.Domain.csproj", "Testcase.User.Domain/"]
RUN dotnet restore "Testcase.API/Testcase.API.csproj"
COPY . .
WORKDIR "/src/Testcase.API"
RUN dotnet build "Testcase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Testcase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testcase.API.dll"]
