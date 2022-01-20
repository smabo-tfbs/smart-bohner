FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ["SmartBohner.Web/Server/bin/Release/net6.0/linux-arm/publish", "App/"]
WORKDIR /App
ENTRYPOINT ["dotnet", "SmartBohner.Web.Server.dll"]