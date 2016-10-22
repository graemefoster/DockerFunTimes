dotnet restore src/DockerFunTimes
dotnet publish src/DockerFunTimes
cp ./scripts/waitforit.sh ./src/DockerFunTimes/bin/Debug/netcoreapp1.0/publish/
CHMOD +x ./src/DockerFunTimes/bin/Debug/netcoreapp1.0/publish/waitforit.sh
docker build ./src/DockerFunTimes/ -t aspnetcoreondocker

dotnet restore src/DockerFunRabbitReader
dotnet publish src/DockerFunRabbitReader
cp ./scripts/waitforit.sh ./src/DockerFunRabbitReader/bin/Debug/netcoreapp1.0/publish/
CHMOD +x ./src/DockerFunRabbitReader/bin/Debug/netcoreapp1.0/publish/waitforit.sh
docker build ./src/DockerFunRabbitReader/ -t graemesrabbitreader

