FROM microsoft/dotnet:latest
COPY ./bin/Debug/netcoreapp1.0/publish/waitforit.sh /
COPY ./bin/Debug/netcoreapp1.0/publish/ /root/
ENTRYPOINT dotnet /root/dockerfunrabbitreader.dll
