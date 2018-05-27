FROM nginx
ARG source=.
RUN rm -rf /usr/share/nginx/html/*
COPY nginx.docker.conf /etc/nginx/conf.d/
COPY $source /usr/share/nginx/html
EXPOSE 80