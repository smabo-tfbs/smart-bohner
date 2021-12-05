FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY src/SmartBohner.Web/Server/bin/Release/net6.0/publish App/
WORKDIR /App
ENTRYPOINT ["dotnet", "SmartBohner.Web.Server.dll"]