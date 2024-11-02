FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["HRMWeb.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet build "HRMWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRMWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Docker

EXPOSE 80

ENTRYPOINT ["dotnet", "HRMWeb.dll"]