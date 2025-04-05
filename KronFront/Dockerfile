# Build stage
FROM node:18-alpine as build

WORKDIR /app

# Copy package files
COPY KronFront/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application
COPY KronFront/ .

# Build the application
RUN npm run build

# Production stage
FROM node:18-alpine

WORKDIR /app

# Copy built assets from build stage
COPY --from=build /app/.output ./

# Expose the port the app runs on
EXPOSE 3000

# Start the application
CMD ["node", "./server/index.mjs"] 