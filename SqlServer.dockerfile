FROM mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04

ENV ACCEPT_EULA="Y" \
    SA_PASSWORD="ABC!@#321a"

EXPOSE 11433