using System;
using Newtonsoft.Json;

namespace Mobile.RefApp.Lib.Graph.Mailbox
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/mailboxsettings?view=graph-rest-1.0
    /// 
    /// Settings for the primary mailbox of the signed-in user.
    /// </summary>
    public class MailboxSettings
    {
        /// <summary>
        /// Folder ID of an archive folder for the user.
        /// </summary>
        /// <value>The capability status.</value>
        [JsonProperty("archiveFolder")]
        public string ArchiveFolder { get; set; }

        /// <summary>
        /// Configuration settings to automatically notify the sender of an incoming email with a message from the signed-in user.
        /// </summary>
        /// <value>The capability status.</value>
        [JsonProperty("automaticRepliesSetting", NullValueHandling = NullValueHandling.Ignore)]
        public AutomaticRepliesSetting AutomaticRepliesSetting { get; set; }

        [JsonProperty("The default time zone for the user's mailbox.")]
        public string TimeZone { get; set; }

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public LocaleInfo Language { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/automaticrepliessetting?view=graph-rest-1.0
    /// 
    /// Configuration settings to automatically notify the sender of an incoming email with a message from the signed-in user. For example, an automatic reply to notify that the signed-in user is unavailable to respond to emails.
    /// </summary>
    public class AutomaticRepliesSetting
    {
        /// <summary>
        /// The automatic reply to send to the specified external audience, if Status is AlwaysEnabled or Scheduled.
        /// </summary>
        [JsonProperty("externalReplyMessage")]
        public string ExternalReplyMessage { get; set; }

        /// <summary>
        /// The automatic reply to send to the audience internal to the signed-in user's organization, if Status is AlwaysEnabled or Scheduled.
        /// </summary>
        [JsonProperty("internalReplyMessage")]
        public string InternalReplyMessage { get; set; }

        /// <summary>
        /// The date and time that automatic replies are set to end, if Status is set to Scheduled.
        /// </summary>
        /// <value>The scheduled end date time.</value>
        [JsonProperty("scheduledEndDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ScheduledEndDateTime { get; set; }

        /// <summary>
        /// The date and time that automatic replies are set to begin, if Status is set to Scheduled.
        /// </summary>
        /// <value>The scheduled end date time.</value>
        [JsonProperty("scheduledStartDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ScheduledStartDateTime { get; set; }

        /// <summary>
        /// Configurations status for automatic replies. The possible values are: disabled, alwaysEnabled, scheduled.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; set; }

    }
}
