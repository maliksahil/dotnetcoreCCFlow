# az login
# you'll need to specify your own identifier-uri, and update your appsettings.json in both projects accordingly.
az ad app create --display-name newappname --password e791fcb9-6234-4327-bd01-bd43754ca15f

# consent to app
# TODO figure out how to get app id in a way that it can be sent as parameter here, for now just run this manually
#az ad app permission admin-consent --id "[?displayName == 'newappname'].appId"

# Get the client ID 
az ad app list --query "[?displayName == 'newappname'].appId"