FROM node:18-alpine AS build
WORKDIR /app

# Copy package files
COPY package*.json ./
RUN npm install

# Mettre à jour vue-tsc et vite
RUN npm install vue-tsc@latest --save-dev
RUN npm install vite@latest --save-dev

# Vérifier le cache npm en créant un nouveau projet Vite
RUN npm create vite@latest .

# Copy source files
COPY . .

# Build the application
RUN npm run build

# Production stage
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]