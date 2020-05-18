# Xamarin Ref App Android Readme

### Setup for building will depend if you want to support InTune, Azure AD, and Broker.  To support these features:

- Update AndroidManifest.xml using your app information registered (you want to register your own Package Name, etc)
- Broker setup is complex - you must replace add in a keystore file and setup your debug sessions to use this keystore file
- For Broker setup - your redirect must be registered based on your keystore file (see documentation below - 99% of setup problems can be fixed by following directions properly)
- Update Mobile.RefApp.CoreUI.Assets.Data.Endpoints.json with your app information
- To use SharePoint examples - you need SharePoint Online and your Azure AD AppId must have permissions to query SharePoint Online via Rest or Graph API
---

It is recommended you read ADAL documentation and InTune SDK documenation for detailed documenation on requirements.

- ADAL .NET Documentation [Github](https://github.com/AzureAD/azure-activedirectory-library-for-dotnet/wiki)
- ADAL .NET Broker Setup for Xamarin [Github](https://github.com/AzureAD/azure-activedirectory-library-for-dotnet/wiki/leveraging-brokers-on-Android-and-iOS)
- InTune SDK for Xamarin Bindings [Microsoft Docs](https://docs.microsoft.com/en-us/intune/app-sdk-xamarin)  

Note that InTune is a binding project, so you must follow both the Native iOS and Andrdoid documentation.  This project tries to map these SDK's to a common Interface, but many times this means some methods are available on one platform, but not the other.

