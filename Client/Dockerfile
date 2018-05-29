FROM nginx
ARG DEBIAN_FRONTEND=noninteractive
RUN apt-get update && apt-get install -y iputils-ping
RUN rm -rf /usr/share/nginx/html/*
COPY nginx.docker.conf /etc/nginx/conf.d/
COPY dist /usr/share/nginx/html
EXPOSE 80