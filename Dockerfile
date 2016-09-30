FROM microsoft/dotnet:latest
COPY ./src/bin/Debug/netcoreapp1.0/publish/ /root/
EXPOSE 5000/tcp
ENTRYPOINT dotnet /root/dockerfuntimes.dll