# Ng6AspCore

cd Api
docker build . -t ng6aspcore

cd Client
docker build . -t ng6

docker run -it --rm -p 5000:80 ng6aspcore

minikube start --registry-mirror=https://registry.docker-cn.com

minikube dashboard

kubectl apply -f api-namespace.yml

kubectl apply -f api-deployment.yml

kubectl apply -f api-service.yml

kubectl get pods --namespace=k8s-ng-aspnetcore

kubectl delete service ng6aspcore --namespace=k8s-ng-aspnetcore

kubectl delete deployment ng6aspcore --namespace=k8s-ng-aspnetcore