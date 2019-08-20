# az login
# you'll need to specify your own identifier-uri, and update your appsettings.json in both projects accordingly.
az ad app create \\
    --display-name sampleApp \\
    --identifier-uris https://sahilmalikgmail.onmicrosoft.com/api  \\
    --oauth2-allow-implicit-flow true  \\
    --reply-urls "https://localhost:44377/"  \\
    --password clientsecretpasswordmakethisrandom

# Get the client ID 
az ad app list --query "[?displayName == 'sampleApp'].appId"