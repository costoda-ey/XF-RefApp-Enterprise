using System;
using Newtonsoft.Json;
using Mobile.RefApp.Lib.Graph.Mailbox;

namespace Mobile.RefApp.Lib.Graph.User
{
    public class UserAccount
    {
        /// <summary>
        /// A freeform text entry field for the user to describe themselves.
        /// </summary>
        /// <value>The about me.</value>
        [JsonProperty("aboutMe")]
        public string AboutMe { get; set; }

        /// <summary>
        /// true if the account is enabled; otherwise, false. This property is required when a user is created. Supports $filter.
        /// </summary>
        [JsonProperty("accountEnabled")]
        public bool AccountEnabled { get; set; }

        /// <summary>
        /// Sets the age group of the user. Allowed values: null, minor, notAdult and adult. Refer to the legal age group property definitions for further information.
        /// </summary>
        [JsonProperty("ageGroup")]
        public string AgeGroup { get; set; }

        /// <summary>
        /// Sets the age group of the user. Allowed values: null, minor, notAdult and adult. Refer to the legal age group property definitions for further information.
        /// </summary>
        [JsonProperty("assignedLicenses", NullValueHandling = NullValueHandling.Ignore)]
        public AssignedLicense[] AssignedLicenses { get; set; }

        /// <summary>
        /// The plans that are assigned to the user. Read-only. Not nullable.
        /// </summary>
        [JsonProperty("assignedPlans", NullValueHandling = NullValueHandling.Ignore)]
        public AssignedPlan[] AssignedPlans { get; set; }

        /// <summary>
        /// The birthday of the user. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'
        /// </summary>
        [JsonProperty("birthday")]
        public DateTimeOffset Birthday { get; set; }

        /// <summary>
        /// The telephone numbers for the user. NOTE: Although this is a string collection, only one number can be set for this property.
        /// </summary>
        [JsonProperty("businessPhones")]
        public string[] BusinessPhones { get; set; }

        /// <summary>
        /// The city in which the user is located. Supports $filter.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// The company name which the user is associated. This property can be useful for describing the company that an external user comes from.
        /// </summary>
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Sets whether consent has been obtained for minors. Allowed values: null, granted, denied and notRequired. Refer to the legal age group property definitions for further information.
        /// </summary>
        [JsonProperty("consentProvidedForMinor")]
        public string ConsentProvidedForMinor { get; set; }

        /// <summary>
        /// The country/region in which the user is located; for example, “US” or “UK”. Supports $filter.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// The created date of the user object.
        /// </summary>
        [JsonProperty("createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// The name for the department in which the user works. Supports $filter.
        /// </summary>
        [JsonProperty("department")]
        public string Department { get; set; }

        /// <summary>
        /// The name displayed in the address book for the user. This is usually the combination of the user's first name, middle initial and last name. This property is required when a user is created and it cannot be cleared during updates. Supports $filter and $orderby.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The employee identifier assigned to the user by the organization. Supports $filter.        
        /// </summary>
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// The fax number of the user.
        /// </summary>
        [JsonProperty("faxNumber")]
        public string FaxNumber { get; set; }

        [JsonIgnore]
        public string FullName => $"{GivenName} {Surname}";

        /// <summary>
        /// The given name (first name) of the user. Supports $filter.
        /// </summary>
        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        /// <summary>
        /// The hire date of the user. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'
        /// </summary>
        [JsonProperty("hireDate")]
        public DateTimeOffset HireDate { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The instant message voice over IP (VOIP) session initiation protocol (SIP) addresses for the user. Read-only.
        /// </summary>
        [JsonProperty("imAddresses")]
        public string[] IMAddresses { get; set; }

        /// <summary>
        /// A list for the user to describe their interests.
        /// </summary>
        [JsonProperty("interests")]
        public string[] Interests { get; set; }

        /// <summary>
        /// true if the user is a resource account; otherwise, false. Null value should be considered false.
        /// </summary>
        [JsonProperty("isResourceAccount")]
        public bool IsResourceAccount { get; set; }

        /// <summary>
        /// The user’s job title. Supports $filter.
        /// </summary>
        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        /// <summary>
        /// Used by enterprise applications to determine the legal age group of the user. This property is read-only and calculated based on ageGroup and consentProvidedForMinor properties. Allowed values: null, minorWithOutParentalConsent, minorWithParentalConsent, minorNoParentalConsentRequired, notAdult and adult. Refer to the legal age group property definitions for further information.)
        /// </summary>
        [JsonProperty("legalAgeGroupClassification")]
        public string LegalAgeGroupClassification { get; set; }

        /// <summary>
        /// The SMTP address for the user, for example, "jeff@contoso.onmicrosoft.com". Read-Only. Supports $filter.
        /// </summary>
        [JsonProperty("mail")]
        public string Mail { get; set; }

        /// <summary>
        /// The mail alias for the user. This property must be specified when a user is created. Supports $filter.
        /// </summary>
        [JsonProperty("mailNickname")]
        public string MailNickname { get; set; }

        /// <summary>
        /// Settings for the primary mailbox of the signed-in user. You can get or update settings for sending automatic replies to incoming messages, locale and time zone.
        /// </summary>
        [JsonProperty("mailboxSettings", NullValueHandling = NullValueHandling.Ignore)]
        public MailboxSettings MailboxSettings { get; set; }

        /// <summary>
        /// The primary cellular telephone number for the user.
        /// </summary>
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// The URL for the user's personal site.
        /// </summary>
        [JsonProperty("mySite")]
        public string MySite { get; set; }

        /// <summary>
        /// The office location in the user's place of business.
        /// </summary>
        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }

        /// <summary>
        /// Contains the on-premises Active Directory distinguished name or DN. The property is only populated for customers who are synchronizing their on-premises directory to Azure Active Directory via Azure AD Connect. Read-only.
        /// </summary>
        [JsonProperty("onPremisesDistinguishedName")]
        public string OnPremisesDistinguishedName { get; set; }

        /// <summary>
        /// Contains the on-premises domainFQDN, also called dnsDomainName synchronized from the on-premises directory.The property is only populated for customers who are synchronizing their on-premises directory to Azure Active Directory via Azure AD Connect. Read-only.
        /// </summary>
        [JsonProperty("onPremisesDomainName")]
        public string OnPremisesDomainName { get; set; }

        /// <summary>
        /// This property is used to associate an on-premises Active Directory user account to their Azure AD user object. This property must be specified when creating a new user account in the Graph if you are using a federated domain for the user’s userPrincipalName (UPN) property. Important: The $ and _ characters cannot be used when specifying this property. Supports $filter.
        /// </summary>
        [JsonProperty("onPremisesImmutableId")]
        public string OnPremisesImmutableId { get; set; }

        /// <summary>
        /// Indicates the last time at which the object was synced with the on-premises directory; for example: "2013-02-16T03:04:54Z". The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'. Read-only.
        /// </summary>
        [JsonProperty("onPremisesLastSyncDateTime")]
        public DateTimeOffset OnPremisesLastSyncDateTime { get; set; }

        [JsonIgnore]
        public string OnPremisesLastSyncDateTimeDisplay => OnPremisesLastSyncDateTime.ToLocalTime().ToString();

        /// <summary>
        /// Contains the on-premises samAccountName synchronized from the on-premises directory. The property is only populated for customers who are synchronizing their on-premises directory to Azure Active Directory via Azure AD Connect. Read-only.
        /// </summary>
        [JsonProperty("onPremisesSamAccountName")]
        public string OnPremisesSamAccountName { get; set; }

        /// <summary>
        /// Contains the on-premises security identifier (SID) for the user that was synchronized from on-premises to the cloud. Read-only.
        /// </summary>
        [JsonProperty("onPremisesSecurityIdentifier")]
        public string OnPremisesSecurityIdentifier { get; set; }

        /// <summary>
        /// true if this object is synced from an on-premises directory; false if this object was originally synced from an on-premises directory but is no longer synced; null if this object has never been synced from an on-premises directory (default). Read-only
        /// </summary>
        [JsonProperty("onPremisesSyncEnabled")]
        public bool OnPremisesSyncEnabled { get; set; }

        /// <summary>
        /// Contains the on-premises userPrincipalName synchronized from the on-premises directory. The property is only populated for customers who are synchronizing their on-premises directory to Azure Active Directory via Azure AD Connect. Read-only.
        /// </summary>
        [JsonProperty("onPremisesUserPrincipalName")]
        public string OnPremisesUserPrincipalName { get; set; }

        /// <summary>
        /// A list of additional email addresses for the user; for example: ["bob@contoso.com", "Robert@fabrikam.com"]. Supports $filter.
        /// </summary>
        [JsonProperty("otherMails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] OtherMails { get; set; }

        /// <summary>
        /// Specifies password policies for the user. This value is an enumeration with one possible value being “DisableStrongPassword”, which allows weaker passwords than the default policy to be specified. “DisablePasswordExpiration” can also be specified. The two may be specified together; for example: "DisablePasswordExpiration, DisableStrongPassword".
        /// </summary>
        [JsonProperty("passwordPolicies")]
        public string PasswordPolicies { get; set; }

        /// <summary>
        /// A list for the user to enumerate their past projects.
        /// </summary>
        [JsonProperty("pastProjects")]
        public string[] PastProjects { get; set; }

        /// <summary>
        /// The postal code for the user's postal address. The postal code is specific to the user's country/region. In the United States of America, this attribute contains the ZIP code.
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The preferred data location for the user. For more information, see OneDrive Online Multi-Geo (https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/multigeo-introduction).
        /// </summary>
        [JsonProperty("preferredDataLocation")]
        public string PreferredDataLocation { get; set; }

        /// <summary>
        /// The preferred language for the user. Should follow ISO 639-1 Code; for example "en-US".
        /// </summary>
        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }

        /// <summary>
        /// The preferred name for the user.
        /// </summary>
        [JsonProperty("preferredName")]
        public string PreferredName { get; set; }

        /// <summary>
        /// For example: ["SMTP: bob@contoso.com", "smtp: bob@sales.contoso.com"] The any operator is required for filter expressions on multi-valued properties. Read-only, Not nullable. Supports $filter.
        /// </summary>
        [JsonProperty("proxyAddresses")]
        public string[] ProxyAddresses { get; set; }

        /// <summary>
        /// A list for the user to enumerate their responsibilities.
        /// </summary>
        [JsonProperty("responsibilities")]
        public string[] Responsibilities { get; set; }

        /// <summary>
        /// A list for the user to enumerate the schools they have attended.
        /// </summary>
        [JsonProperty("schools")]
        public string[] Schools { get; set; }

        /// <summary>
        /// true if the Outlook global address list should contain this user, otherwise false. If not set, this will be treated as true. For users invited through the invitation manager, this property will be set to false.
        /// </summary>
        [JsonProperty("showInAddressList")]
        public bool showInAddressList { get; set; }

        /// <summary>
        /// A list for the user to enumerate their skills.
        /// </summary>
        [JsonProperty("skills")]
        public string[] Skills { get; set; }

        /// <summary>
        /// Any refresh tokens or sessions tokens (session cookies) issued before this time are invalid, and applications will get an error when using an invalid refresh or sessions token to acquire a delegated access token (to access APIs such as Microsoft Graph). If this happens, the application will need to acquire a new refresh token by making a request to the authorize endpoint. Read-only. Use revokeSignInSessions to reset: https://docs.microsoft.com/en-us/graph/api/user-revokesigninsessions?view=graph-rest-1.0
        /// </summary>
        [JsonProperty("signInSessionsValidFromDateTime")]
        public DateTimeOffset SignInSessionsValidFromDateTime { get; set; }

        /// <summary>
        /// The state or province in the user's address. Supports $filter.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// The street address of the user's place of business.
        /// </summary>
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// The user's surname (family name or last name). Supports $filter.
        /// </summary>
        [JsonProperty("surname")]
        public string Surname { get; set; }

        /// <summary>
        /// A two letter country code (ISO standard 3166). Required for users that will be assigned licenses due to legal requirement to check for availability of services in countries. Examples include: "US", "JP", and "GB". Not nullable. Supports $filter.
        /// </summary>
        [JsonProperty("usageLocation")]
        public string UsageLocation { get; set; }

        /// <summary>
        /// The user principal name (UPN) of the user. The UPN is an Internet-style login name for the user based on the Internet standard RFC 822. By convention, this should map to the user's email name. The general format is alias@domain, where domain must be present in the tenant’s collection of verified domains. This property is required when a user is created. The verified domains for the tenant can be accessed from the verifiedDomains property of organization. Supports $filter and $orderby.
        /// </summary>
        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// A string value that can be used to classify user types in your directory, such as “Member” and “Guest”. Supports $filter.
        /// </summary>
        [JsonProperty("userType")]
        public string UserType { get; set; }

    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/assignedlicense?view=graph-rest-1.0
    /// 
    /// Represents a license assigned to a user. The assignedLicenses property of the user entity is a collection of assignedLicense.
    /// </summary>
    public class AssignedLicense
    {
        /// <summary>
        /// A collection of the unique identifiers for plans that have been disabled.
        /// </summary>
        [JsonProperty("disabledPlans", NullValueHandling = NullValueHandling.Ignore)]
        public Guid[] DisabledPlans { get; set; }

        /// <summary>
        /// The unique identifier for the SKU.
        /// </summary>
        [JsonProperty("skuId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid SKUId { get; set; }

        [JsonIgnore]
        public string Display => $"SKU: {SKUId}";
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/assignedplan?view=graph-rest-1.0
    /// 
    /// The assignedPlans property of both the user entity and the organization entity is a collection of assignedPlan.
    /// </summary>
    public class AssignedPlan
    {
        /// <summary>
        /// The date and time at which the plan was assigned; for example: 2013-01-02T19:32:30Z. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'
        /// </summary>
        [JsonProperty("assignedDateTime")]
        public DateTimeOffset AssignedDateTime { get; set; }

        /// <summary>
        /// For example, “Enabled”.
        /// </summary>
        [JsonProperty("capabilityStatus")]
        public string CapabilityStatus { get; set; }

        /// <summary>
        /// The name of the service; for example, “Exchange”.
        /// </summary>
        [JsonProperty("service")]
        public string Service { get; set; }

        /// <summary>
        /// A GUID that identifies the service plan.
        /// </summary>
        [JsonProperty("servicePlanId")]
        public Guid ServicePlanId { get; set; }

        [JsonIgnore]
        public string Display => $"Assigned Date: {AssignedDateTime.ToLocalTime()}{Environment.NewLine}Service: {Service}{Environment.NewLine}Status: {CapabilityStatus}";
    }
}
