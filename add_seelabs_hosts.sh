#!/bin/bash

# Define the IP address and hostname
IP_ADDRESS="103.233.100.16"
HOSTNAME="see.labs.telkomuniversity.ac.id"
HOST_ENTRY="$IP_ADDRESS $HOSTNAME"

# Check if the entry already exists in the /etc/hosts file
if grep -q "$HOSTNAME" /etc/hosts; then
    echo "The hostname $HOSTNAME already exists in /etc/hosts."
else
    # Add the entry to the /etc/hosts file
    echo "Adding $HOST_ENTRY to /etc/hosts"
    echo "$HOST_ENTRY" | sudo tee -a /etc/hosts > /dev/null
    echo "Entry added successfully."
fi
