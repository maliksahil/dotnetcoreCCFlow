# az login
# you'll need to specify your own identifier-uri, and update your appsettings.json in both projects accordingly.
az ad app create \\
    --display-name sampleApp \\
    --identifier-uris https://[TENANT].onmicrosoft.com/api  \\
    --password clientsecretpasswordmakethisrandom

# Get the client ID 
az ad app list --query "[?displayName == 'sampleApp'].appId"