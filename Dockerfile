FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Practica2Api/Practica2Api.csproj Practica2Api/
RUN dotnet restore Practica2Api/Practica2Api.csproj

COPY Practica2Api/ Practica2Api/
WORKDIR /src/Practica2Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Practica2Api.dll"]