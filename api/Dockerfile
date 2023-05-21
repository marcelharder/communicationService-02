FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY Release App/
WORKDIR /App

EXPOSE 587/tcp

ENTRYPOINT [ "dotnet", "api.dll" ]