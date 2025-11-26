FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Streaminds.Web/Streaminds.Web.csproj", "Streaminds.Web/"]
COPY ["Streaminds.Domain/Streaminds.Domain.csproj", "Streaminds.Domain/"]
COPY ["Streaminds.Infrastructure/Streaminds.Infrastructure.csproj", "Streaminds.Infrastructure/"]

RUN dotnet restore "Streaminds.Web/Streaminds.Web.csproj"

COPY . .
WORKDIR "/src/Streaminds.Web"
RUN dotnet build "Streaminds.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Streaminds.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Streaminds.Web.dll"]
