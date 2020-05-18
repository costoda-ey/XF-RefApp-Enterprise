using System;
using Newtonsoft.Json;

namespace Mobile.RefApp.Lib.Graph
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/datetimetimezone?view=graph-rest-1.0
    /// 
    /// Describes the date, time, and time zone of a point in time.
    /// </summary>
    public class DateTimeTimeZone
    {
        /// <summary>
        /// A single point of time in a combined date and time representation ({date}T{time}; for example, 2017-08-29T04:00:00.0000000).
        /// </summary>
        /// <value>The date and time.</value>
        [JsonProperty("dateTime")]
        public string DateAndTime { get; set; }

        /// <summary>
        /// The TimeZone property can be set to any of the time zones supported by Windows, as well as the following time zones names:  Etc/GMT+12, Etc/GMT+11, Pacific/Honolulu, America/Anchorage, America/Santa_Isabel, America/Los_Angeles, America/Phoenix, America/Chihuahua, America/Denver, America/Guatemala, America/Chicago, America/Mexico_City, America/Regina, America/Bogota, America/New_York, America/Indiana/Indianapolis, America/Caracas, America/Asuncion, America/Halifax, America/Cuiaba, America/La_Paz, America/Santiago, America/St_Johns, America/Sao_Paulo, America/Argentina/Buenos_Aires, America/Cayenne, America/Godthab, America/Montevideo, America/Bahia, Etc/GMT+2, Atlantic/Azores, Atlantic/Cape_Verde, Africa/Casablanca, Etc/GMT, Europe/London, Atlantic/Reykjavik, Europe/Berlin, Europe/Budapest, Europe/Paris, Europe/Warsaw, Africa/Lagos, Africa/Windhoek, Europe/Bucharest, Asia/Beirut, Africa/Cairo, Asia/Damascus, Africa/Johannesburg, Europe/Kyiv (Kiev), Europe/Istanbul, Asia/Jerusalem, Asia/Amman, Asia/Baghdad, Europe/Kaliningrad, Asia/Riyadh, Africa/Nairobi, Asia/Tehran, Asia/Dubai, Asia/Baku, Europe/Moscow, Indian/Mauritius, Asia/Tbilisi, Asia/Yerevan, Asia/Kabul, Asia/Karachi, Asia/Toshkent (Tashkent), Asia/Kolkata, Asia/Colombo, Asia/Kathmandu, Asia/Astana (Almaty), Asia/Dhaka, Asia/Yekaterinburg, Asia/Yangon (Rangoon), Asia/Bangkok, Asia/Novosibirsk, Asia/Shanghai, Asia/Krasnoyarsk, Asia/Singapore, Australia/Perth, Asia/Taipei, Asia/Ulaanbaatar, Asia/Irkutsk, Asia/Tokyo, Asia/Seoul, Australia/Adelaide, Australia/Darwin, Australia/Brisbane, Australia/Sydney, Pacific/Port_Moresby, Australia/Hobart, Asia/Yakutsk, Pacific/Guadalcanal, Asia/Vladivostok, J Pacific/Auckland, Etc/GMT-12, Pacific/Fiji, Asia/Magadan, Pacific/Tongatapu, Pacific/Apia, Pacific/Kiritimati
        /// </summary>
        /// <value>The time zone.</value>
        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/localeinfo?view=graph-rest-1.0
    /// 
    /// Information about the locale, including the preferred language and country/region, of the signed-in user.
    /// </summary>
    public class LocaleInfo
    {
        /// <summary>
        /// A locale representation for the user, which includes the user's preferred language and country/region. For example, "en-us". The language component follows 2-letter codes as defined in ISO 639-1, and the country component follows 2-letter codes as defined in ISO 3166-1 alpha-2.
        /// </summary>
        /// <value>The locale.</value>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// A name representing the user's locale in natural language, for example, "English (United States)".
        /// </summary>
        /// <value>The display name.</value>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
