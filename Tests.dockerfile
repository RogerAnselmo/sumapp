FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
COPY . /app

WORKDIR /app/tests/SumApp.UnitTests
RUN dotnet restore
RUN dotnet test

WORKDIR /app/tests/SumApp.IntegrationTests
RUN dotnet restore
RUN dotnet test