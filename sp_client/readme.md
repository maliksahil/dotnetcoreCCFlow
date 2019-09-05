# Service principal authentication

This readme.md explains how to do service principal authentication instead of a basic client secret.

1. Setup an application that represents your API `az ad app create --display-name sampleApp --identifier-uris https://sahilmalikgmail.onmicrosoft.com/api` 
2. Note down the created client ID, and update that in the appsettings.json of your API projects.
3. Create a service principal, `az ad sp create-for-rbac --name dellaterserviceprincipal note down the created appid (username) and password`
4. Get an access token using the service principal 
```
curl -X POST -d 'grant_type=client_credentials&client_id=[appid]&client_secret=[password]&resource=https://sahilmalikgmail.onmicrosoft.com/api' https://login.microsoftonline.com/[tenantid]/oauth2/token
```
5. Make a call using this retrieved access token,  
```
curl -X GET \
  http://localhost:1040/api/todolist \
  -H 'Accept: */*' \
  -H 'Accept-Encoding: gzip, deflate' \
  -H 'Authorization: Bearer <paste_access_token_here>' \
```

Verify that you are able to call this API without setting up any consents or generating any client secrets etc.