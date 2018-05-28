FROM microsoft/aspnetcore:latest
WORKDIR /app
EXPOSE 80
COPY bin/Debug/netcoreapp2.0/publish/ .
ENTRYPOINT ["dotnet", "Api.dll"]