FROM microsoft/dotnet:latest
COPY ./bin/Debug/netcoreapp1.0/publish/waitforit.sh /
COPY ./bin/Debug/netcoreapp1.0/publish/ /root/
EXPOSE 5000/tcp
ENTRYPOINT dotnet /root/dockerfuntimes.dll
