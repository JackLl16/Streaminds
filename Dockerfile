# 1. Usar la imagen del SDK de .NET 10 (Preview/RC si acaba de salir)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 2. Copiar archivos csproj
COPY ["Streaminds.Web/Streaminds.Web.csproj", "Streaminds.Web/"]
COPY ["Streaminds.Domain/Streaminds.Domain.csproj", "Streaminds.Domain/"]
COPY ["Streaminds.Infrastructure/Streaminds.Infrastructure.csproj", "Streaminds.Infrastructure/"]

# 3. Restaurar dependencias
RUN dotnet restore "Streaminds.Web/Streaminds.Web.csproj"

# 4. Copiar el resto y compilar
COPY . .
WORKDIR "/src/Streaminds.Web"
RUN dotnet build "Streaminds.Web.csproj" -c Release -o /app/build

# 5. Publicar
FROM build AS publish
RUN dotnet publish "Streaminds.Web.csproj" -c Release -o /app/publish

# 6. Imagen final con .NET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Streaminds.Web.dll"]
