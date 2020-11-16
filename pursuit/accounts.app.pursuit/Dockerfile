FROM node:alpine3.12 as build
WORKDIR /app
COPY package.json ./
COPY yarn.lock ./
RUN yarn --pure-lockfile
COPY public ./public
COPY src ./src
RUN yarn build

FROM nginx:stable-alpine
COPY --from=build /app/build /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 5003
CMD ["nginx", "-g", "daemon off;"]