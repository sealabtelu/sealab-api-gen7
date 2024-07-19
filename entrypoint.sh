#!/bin/sh

# Replace placeholders in appsettings.json with environment variables
sed -i "s/{DB_SERVER}/${DB_SERVER}/g" /app/appsettings.json
sed -i "s/{DB_PORT}/${DB_PORT}/g" /app/appsettings.json
sed -i "s/{DB_DATABASE}/${DB_DATABASE}/g" /app/appsettings.json
sed -i "s/{DB_USER}/${DB_USER}/g" /app/appsettings.json
sed -i "s/{DB_PASSWORD}/${DB_PASSWORD}/g" /app/appsettings.json
sed -i "s/{WEB_URL}/${WEB_URL}/g" /app/appsettings.json
sed -i "s/{SMTP_SERVER}/${SMTP_SERVER}/g" /app/appsettings.json
sed -i "s/{SMTP_USERNAME}/${SMTP_USERNAME}/g" /app/appsettings.json
sed -i "s/{SMTP_PASSWORD}/${SMTP_PASSWORD}/g" /app/appsettings.json

# Start the application
exec dotnet SealabAPI.dll
