FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

COPY ./src /src

WORKDIR /src/SumApp.API

RUN dotnet restore
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY --from=build /src/SumApp.API/publish /app

WORKDIR /app

ENV ASPNETCORE_URLS="http://*:5000"
EXPOSE 5000

ENTRYPOINT [ "dotnet", "/app/SumApp.API.dll" ]