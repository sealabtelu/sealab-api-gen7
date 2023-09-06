#!/bin/bash

# Menjalankan git pull dari branch 'dev' di repository remote 'origin'
git pull origin dev

# Melakukan publish aplikasi .NET dengan konfigurasi release
dotnet publish -c release

# Menjalankan perintah EF Core untuk melakukan database update
dotnet ef database update

# Navigasi ke direktori publish
cd bin/Release/net6.0/publish

# Menjalankan aplikasi .NET
dotnet SealabAPI.dll
