FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY . /app

WORKDIR /app/tests/SumApp.UnitTests
RUN dotnet restore
RUN dotnet test

ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.5.0/wait /wait
RUN chmod +x /wait

WORKDIR /app/tests/SumApp.IntegrationTests
RUN dotnet restore
CMD /wait && dotnet test