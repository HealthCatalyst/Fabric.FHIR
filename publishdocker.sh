@echo off
read -n 1 -p 'Are you sure you want to publish to dockerhub?'
echo "(ok)"

echo Logging in to dockerhub as healthcatalyst  
docker login --username healthcatalyst

docker stop fabric.fhir
docker rm fabric.fhir

cd fabric.fhir
dotnet publish --configuration Release --output obj/Docker/publish

docker build -t healthcatalyst/fabric.fhir .
docker push healthcatalyst/fabric.fhir
cd ..
echo Press any key to exit
read -n 1
