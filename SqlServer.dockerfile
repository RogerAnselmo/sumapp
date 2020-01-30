FROM mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf

ENV ACCEPT_EULA="Y" \
    SA_PASSWORD="ABC!@#321a"

EXPOSE 1433