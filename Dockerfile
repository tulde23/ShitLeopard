# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 as base

WORKDIR /app
EXPOSE 5001
EXPOSE 5002
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT=Development



FROM base as publish
COPY /src/ubuntu ./app/publish .
ENTRYPOINT ["dotnet", "ShitLeopard.Api.dll"]
CMD ["--urls  http://*:5001;https://*:5002"]



