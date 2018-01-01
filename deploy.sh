ApiKey=$1
BuildNumber=$2

sed -i "s/1.0.0/1.0.$TRAVIS_BUILD_NUMBER/g" ./PixelHarmony.FluentConsole/PixelHarmony.FluentConsole.csproj
dotnet pack -c Release ./PixelHarmony.FluentConsole/
dotnet nuget push ./PixelHarmony.FluentConsole/bin/Release/PixelHarmony.FluentConsole.1.0.$TRAVIS_BUILD_NUMBER.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json