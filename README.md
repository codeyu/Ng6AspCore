# Ng6AspCore

cd Api
docker build . -t ng6aspcore

cd Client
docker build . -t ng6

docker run -it --rm -p 5000:80 ng6aspcore