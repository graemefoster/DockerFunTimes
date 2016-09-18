cd src
dotnet restore
dotnet publish
docker build -t containerfun /.
