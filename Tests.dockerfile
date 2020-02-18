FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY . /app

WORKDIR /app/tests/SumApp.UnitTests
RUN dotnet restore

ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.7.3/wait /wait
RUN chmod +x /wait

WORKDIR /app/tests/SumApp.IntegrationTests
RUN dotnet restore

WORKDIR /app/tests/SumApp.UnitTests
RUN dotnet test

WORKDIR /app/tests/SumApp.IntegrationTests
CMD /wait && dotnet test