# Xamarin Ref App iOS Readme

### Setup for building will depend if you want to support InTune, Azure AD, and Broker.  To support these features:

- Update Info.plist using your app information registered (you want to register your own App ID bececause of redirects for Broker)
- Update Entitlements.plist (required for ADAL Broker)
- Update Mobile.RefApp.CoreUI.Assets.Data.Endpoints.json with your app information
- For Broker setup, one needs to read through ALL the documentation linked before and make sure everything is setup properly for redirects in AAD (Azure Active Directory)
- To use SharePoint examples - you need SharePoint Online and your Azure AD AppId must have permissions to query SharePoint Online via Rest or Graph API
---

It is recommended you read ADAL documentation and InTune SDK documenation for detailed documenation on requirements.

- ADAL .NET Documentation [Github](https://github.com/AzureAD/azure-activedirectory-library-for-dotnet/wiki)
- ADAL .NET Broker Setup for Xamarin [Github](https://github.com/AzureAD/azure-activedirectory-library-for-dotnet/wiki/leveraging-brokers-on-Android-and-iOS)
- InTune SDK for Xamarin Bindings [Microsoft Docs](https://docs.microsoft.com/en-us/intune/app-sdk-xamarin)  

Note that InTune is a binding project, so you must follow both the Native iOS and Andrdoid documentation.  This project tries to map these SDK's to a common Interface, but many times this means some methods are available on one platform, but not the other.
