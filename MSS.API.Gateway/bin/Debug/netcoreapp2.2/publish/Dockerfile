FROM microsoft/dotnet:2.2-aspnetcore-runtime
 
WORKDIR /app

COPY . .

EXPOSE 5800

ENTRYPOINT ["dotnet", "MSS.API.Gateway.dll"]