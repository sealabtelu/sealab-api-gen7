#!/bin/bash

# Navigasi ke root
cd ../../../../

# Menjalankan git pull dari branch 'dev' di repository remote 'origin'
git pull origin dev

if [ $? -ne 0 ]; then
    echo "Error: Git pull failed"
    exit 1
fi

# Melakukan publish aplikasi .NET dengan konfigurasi release
dotnet publish -c release

if [ $? -ne 0 ]; then
    echo "Error: .NET publish failed"
    exit 2
fi

# Menjalankan perintah EF Core untuk melakukan database update
dotnet ef database update

if [ $? -ne 0 ]; then
    echo "Error: EF Core database update failed"
    exit 3
fi

echo "Deploy success!"