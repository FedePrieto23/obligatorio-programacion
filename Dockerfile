FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Obligatorio_Programacion.csproj .
RUN dotnet restore Obligatorio_Programacion.csproj

COPY . .
RUN dotnet publish Obligatorio_Programacion.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

CMD ["sh", "-c", "dotnet Obligatorio_Programacion.dll --urls http://0.0.0.0:${PORT:-10000}"]