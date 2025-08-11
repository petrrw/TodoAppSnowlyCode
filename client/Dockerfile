FROM node:18-alpine AS build

WORKDIR /app

COPY todoapp-client/package.json todoapp-client/package-lock.json ./

RUN npm ci

COPY todoapp-client/ ./

ARG REACT_APP_BACKENDURL
ENV REACT_APP_BACKENDURL=$REACT_APP_BACKENDURL

RUN npm run build

FROM nginx:alpine

COPY --from=build /app/build /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
