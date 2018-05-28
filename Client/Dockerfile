FROM nginx
RUN rm -rf /usr/share/nginx/html/*
COPY nginx.docker.conf /etc/nginx/conf.d/
COPY dist /usr/share/nginx/html
EXPOSE 80