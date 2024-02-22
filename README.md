# Konfigurasi VPS untuk SeaLab API

Berikut adalah langkah-langkah untuk mengkonfigurasi Virtual Private Server (VPS) untuk menjalankan SeaLab API.

## Instalasi .NET SDK 6.0 dan Entity Framework Core Tools

```bash
sudo apt-get update && sudo apt-get install -y dotnet-sdk-6.0
dotnet tool install --global dotnet-ef --version 7.16
export PATH="$PATH:/root/.dotnet/tools"
```
Setelah instalasi, pastikan bahwa Entity Framework Core Tools dapat diakses dengan menjalankan `dotnet ef`.

## Instalasi PostgreSQL
```bash
sudo apt install postgresql postgresql-contrib
sudo -i -u postgres
psql

// ganti default password untuk default user postgres
ALTER USER postgres WITH PASSWORD 'new_password';

// keluar dari postgres
\q
exit
```

## Instalasi Nginx
```
sudo apt install nginx
```
Tambahkan konfigurasi untuk membatasi ukuran body request di `/etc/nginx/nginx.conf` dengan menambahkan baris berikut:
```
client_max_body_size 10M;
```
Tambahkan alamat IP Seelabs pada file `/etc/hosts`

```
103.233.100.16 see.labs.telkomuniversity.ac.id
```

Buat file konfigurasi baru untuk situs web di `/etc/nginx/sites-available` dengan nama  `sealab-telu.com` lalu isi dengan kode berikut

```
server {
    listen 80;
    server_name sealab-telu.com;

    location / {
        proxy_pass http://localhost:4173;
    }
}
```
Aktifkan konfigurasi dengan membuat link simbolis ke direktori `/etc/nginx/sites-enabled` dengan kode berikut
``` bash
ln -s /etc/nginx/sites-available/sealab-telu.com /etc/nginx/sites-enabled/
```

## Pengaturan zona waktu
```
sudo timedatectl set-timezone Asia/Jakarta
```
setelah itu ketikan `date` dan akan muncul waktu sekarang
```
Thu Feb 22 07:36:45 UTC 2024
```

## Menjalankan apliaksi
Gunakan terminal multiplexer seperti `tmux` untuk menjalankan SeaLab API secara terus menerus:
```bash
tmux new -s sea-api
tmux a -t sea-api
cd sealab-api
chmod +x deploy.sh
./deploy.sh
```
