# Ng6AspCore

docker build Api/bin/Debug/netcoreapp2.0/publish/ -t ng6aspcore

docker build Client/dist/ -t ng6

docker run -it --rm -p 8080:80 ng6aspcore