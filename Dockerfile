#Building Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY *.csproj .
RUN dotnet restore


COPY . .
RUN dotnet publish -c Release -o /app/publish ManoelStore.csproj

#Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

#Expose Kestrel's default HTTP/HTTPS ports in .NET
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "ManoelStore.dll"]