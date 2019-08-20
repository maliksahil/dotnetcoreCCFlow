# .NET Core Client Credential flow

This code example demonstrates how to use client credential flow with Azure AD.
There are 3 parts to this code example,

1. The setup.sh file that sets up an azure AD app for you. Note that you have a choice of setting up two apps, but I am using the same app as both the client and server, so we don't have to deal with consent issues.
2. The api project, which when run will expose an api at http://localhost:1040/api/todolist
3. The client project, which when run will call the API using client credentials flow.

## Setup

1. Ensure you have azure CLI installed and perform an `az login`
2. When in the correct subscription, run the command from setup.sh .. tweak the values accordingly to suit your environment. Of special mention is the app id URI, it defaults to `https://<tenantname>.onmicrosoft.com/api` .. this is tweakable, but you'll need to update your code (both client and server) to match whatever you choose here.
3. Once you have registered the app, update your appsettings.json in both the api project and the client project with the values from the newly registered AAD app.

## Run it
First, run the api project
Second, run the client project

Verify that the client can call the server using a Bearer token.

## Important note ##
This is demo code, as far as logging goes. Of special mention is PII logging is set to true, to help easy debugging.
If you deploy this to prod, ensure you turn that off.