FROM microsoft/dotnet:2.2-aspnetcore-runtime
 
WORKDIR /app

COPY . .

EXPOSE 3851

ENTRYPOINT ["dotnet", "MSS.API.Core.dll","3851"]