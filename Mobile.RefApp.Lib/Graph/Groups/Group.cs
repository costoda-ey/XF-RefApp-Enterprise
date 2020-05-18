using System;

using Mobile.RefApp.Lib.Graph.User;

using Newtonsoft.Json;

namespace Mobile.RefApp.Lib.Graph.Groups
{
    public class GroupResults
    {
        [JsonProperty("value")]
        public Group[] Groups { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/group?view=graph-rest-1.0
    ///
    /// Represents an Azure Active Directory (Azure AD) group, which can be an Office 365 group, or a security group.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Indicates if people external to the organization can send messages to the group. Default value is false. Returned only on $select.
        /// </summary>
        [JsonProperty("allowExternalSenders", NullValueHandling = NullValueHandling.Ignore)]
        public bool AllowExternalSenders { get; set; }

        /// <summary>
        /// The licenses that are assigned to the group. Returned only on $select. Read-only.
        /// </summary>
        [JsonProperty("assignedLicenses", NullValueHandling = NullValueHandling.Ignore)]
        public AssignedLicense[] AssignedLicenses { get; set; }

        /// <summary>
        /// Indicates if new members added to the group will be auto-subscribed to receive email notifications. You can set this property in a PATCH request for the group; do not set it in the initial POST request that creates the group. Default value is false. Returned only on $select.
        /// </summary>
        [JsonProperty("autoSubscribeNewMembers", NullValueHandling = NullValueHandling.Ignore)]
        public bool AutoSubscribeNewMembers { get; set; }

        /// <summary>
        /// Describes a classification for the group (such as low, medium or high business impact). Valid values for this property are defined by creating a ClassificationList setting value, based on the template definition. Returned by default.
        /// https://docs.microsoft.com/en-us/graph/api/resources/groupsetting?view=graph-rest-1.0
        /// https://docs.microsoft.com/en-us/graph/api/resources/groupsettingtemplate?view=graph-rest-1.0
        /// </summary>
        [JsonProperty("classification")]
        public string Classification { get; set; }

        /// <summary>
        /// Timestamp of when the group was created. The value cannot be modified and is automatically populated when the group is created. The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'. Returned by default. Read-only.
        /// </summary>
        [JsonProperty("createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        [JsonIgnore]
        public string CreatedDateTimeDisplay => CreatedDateTime.ToLocalTime().ToString();

        /// <summary>
        /// An optional description for the group. Returned by default.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The display name for the group. This property is required when a group is created and cannot be cleared during updates. Returned by default. Supports $filter and $orderby.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Specifies the group type and its membership. If the collection contains Unified then the group is an Office 365 group; otherwise it's a security group. If the collection includes DynamicMembership, the group has dynamic membership; otherwise, membership is static. Returned by default. Supports $filter.
        /// </summary>
        [JsonProperty("groupTypes")]
        public string[] GroupTypes { get; set; }

        /// <summary>
        ///Indicates whether there are members in this group that have license errors from its group-based license assignment. This property is never returned on a GET operation. You can use it as a $filter argument to get groups that have members with license errors (that is, filter for this property being true). See an example - https://docs.microsoft.com/en-us/graph/api/group-list?view=graph-rest-1.0&tabs=cs 
        /// </summary>
        [JsonProperty("hasMembersWithLicenseErrors", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasMembersWithLicenseErrors { get; set; }

        /// <summary>
        /// The unique identifier for the group. Returned by default. Inherited from directoryObject. Key. Not nullable. Read-only.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Indicates whether the signed-in user is subscribed to receive email conversations. Default value is true. Returned only on $select.
        /// </summary>
        [JsonProperty("isSubscribedByMail", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsSubscribedByMail { get; set; }

        /// <summary>
        ///Indicates status of the group license assignment to all members of the group. Default value is false. Read-only. Possible values: QueuedForProcessing, ProcessingInProgress, and ProcessingComplete. Returned only on $select. Read-only. 
        /// </summary>
        [JsonProperty("licenseProcessingState", NullValueHandling = NullValueHandling.Ignore)]
        public LicenseProcessingState LicenseProcessingState { get; set; }

        /// <summary>
        /// The SMTP address for the group, for example, "serviceadmins@contoso.onmicrosoft.com". Returned by default. Read-only. Supports $filter.
        /// </summary>
        [JsonProperty("mail")]
        public string Mail { get; set; }

        /// <summary>
        /// Specifies whether the group is mail-enabled. Returned by default.
        /// </summary>
        [JsonProperty("mailEnabled")]
        public bool MailEnabled { get; set; }

        /// <summary>
        /// The mail alias for the group, unique in the organization. This property must be specified when a group is created. Returned by default. Supports $filter.
        /// </summary>
        [JsonProperty("mailNickname")]
        public string MailNickname { get; set; }

        /// <summary>
        /// Indicates the last time at which the group was synced with the on-premises directory.The Timestamp type represents date and time information using ISO 8601 format and is always in UTC time. For example, midnight UTC on Jan 1, 2014 would look like this: '2014-01-01T00:00:00Z'. Returned by default. Read-only. Supports $filter.
        /// </summary>
        [JsonProperty("onPremisesLastSyncDateTime")]
        public DateTimeOffset? OnPremisesLastSyncDateTime { get; set; }

        [JsonIgnore]
        public string OnPremisesLastSyncDateTimeDisplay => OnPremisesLastSyncDateTime?.ToLocalTime().ToString();

        /// <summary>
        /// Contains the on-premises security identifier (SID) for the group that was synchronized from on-premises to the cloud. Returned by default. Read-only.
        /// </summary>
        [JsonProperty("onPremisesSecurityIdentifier")]
        public string OnPremisesSecurityIdentifier { get; set; }

        /// <summary>
        /// true if this group is synced from an on-premises directory; false if this group was originally synced from an on-premises directory but is no longer synced; null if this object has never been synced from an on-premises directory (default). Returned by default. Read-only. Supports $filter.
        /// </summary>
        [JsonProperty("onPremisesSyncEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool OnPremisesSyncEnabled { get; set; }

        /// <summary>
        ///     The preferred data location for the group. For more information, see OneDrive Online Multi-Geo. Returned by default.
        /// </summary>
        [JsonProperty("preferredDataLocation")]
        public string PreferredDataLocation { get; set; }

        /// <summary>
        /// Email addresses for the group that direct to the same group mailbox. For example: ["SMTP: bob@contoso.com", "smtp: bob@sales.contoso.com"]. The any operator is required to filter expressions on multi-valued properties. Returned by default. Read-only. Not nullable. Supports $filter.
        /// </summary>
        [JsonProperty("proxyAddresses")]
        public string[] ProxyAddresses { get; set; }

        /// <summary>
        /// Specifies whether the group is a security group. Returned by default. Supports $filter.
        /// </summary>
        [JsonProperty("securityEnabled")]
        public bool SecurityEnabled { get; set; }

        /// <summary>
        /// Count of conversations that have received new posts since the signed-in user last visited the group. Returned only on $select.
        /// </summary>
        [JsonProperty("unseenCount")]
        public int UnseenCount { get; set; }

        /// <summary>
        /// Specifies the visibility of an Office 365 group. Possible values are: private, public, or hiddenmembership; blank values are treated as public. See group visibility options to learn more (https://docs.microsoft.com/en-us/graph/api/resources/group?view=graph-rest-1.0#group-visibility-options). Visibility can be set only when a group is created; it is not editable. Visibility is supported only for unified groups; it is not supported for security groups. Returned by default.
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; set; }

    }

    /// <summary>
    ///Indicates status of the group license assignment to all members of the group. Default value is false. Read-only. Possible values: QueuedForProcessing, ProcessingInProgress, and ProcessingComplete. Returned only on $select. Read-only. 
    /// </summary>
    public class LicenseProcessingState
    {
        /// <summary>
        ///Indicates status of the group license assignment to all members of the group. Default value is false. Read-only. Possible values: QueuedForProcessing, ProcessingInProgress, and ProcessingComplete. Returned only on $select. Read-only. 
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
