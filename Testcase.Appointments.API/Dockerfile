#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Testcase.Appointments.API/Testcase.Appointments.API.csproj", "Testcase.Appointments.API/"]
COPY ["Testcase.Appointments.Infrastructure/Testcase.Appointments.Infrastructure.csproj", "Testcase.Appointments.Infrastructure/"]
COPY ["Testcase.Infrastructure/Testcase.Infrastructure.csproj", "Testcase.Infrastructure/"]
COPY ["Testcase.User.Domain/Testcase.User.Domain.csproj", "Testcase.User.Domain/"]
COPY ["Testcase.Appointments.Domain/Testcase.Appointments.Domain.csproj", "Testcase.Appointments.Domain/"]
RUN dotnet restore "Testcase.Appointments.API/Testcase.Appointments.API.csproj"
COPY . .
WORKDIR "/src/Testcase.Appointments.API"
RUN dotnet build "Testcase.Appointments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Testcase.Appointments.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testcase.Appointments.API.dll"]