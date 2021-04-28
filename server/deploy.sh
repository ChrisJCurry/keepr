dotnet publish -c Release
docker build -t keepr .
docker tag keepr registry.heroku.com/keepr-curry/web
docker push registry.heroku.com/keepr-curry/web
heroku container:release web -a keepr-curry
echo press any key
read end