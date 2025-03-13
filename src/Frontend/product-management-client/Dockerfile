# Stage 1: Build the Vue.js application
FROM node:lts-alpine AS build-stage

# Set working directory
WORKDIR /app

# Copy only package.json and package-lock.json first for better caching
COPY src/Frontend/product-management-client/package*.json ./

# Debug: Check if package.json is copied correctly
RUN ls -l /app

# Install dependencies
RUN npm install

# Copy the full project
COPY src/Frontend/product-management-client ./

# Build the Vue.js app
RUN npm run build

# Debug: Check if the build output exists
RUN ls -l /app/dist

# Stage 2: Serve the Vue app with nginx
FROM nginx:stable-alpine AS production-stage

# Set working directory
WORKDIR /usr/share/nginx/html

# Copy built Vue app from the build stage
COPY --from=build-stage /app/dist ./

# Expose the correct port
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
