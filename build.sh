dotnet restore
dotnet publish
docker build -t containerfun $TRAVIS_BUILD_DIR/Example-4/.

