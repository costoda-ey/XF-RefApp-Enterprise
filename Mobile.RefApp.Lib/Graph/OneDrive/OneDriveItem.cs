using System;
using System.IO;

using Newtonsoft.Json;

namespace Mobile.RefApp.Lib.Graph.OneDrive
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/driveitem?view=graph-rest-1.0
    /// </summary>
    public class OneDriveItem
    {

        /// <summary>
        /// Identity of the user, device, and application which created the item. Read-only.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty("audio", NullValueHandling = NullValueHandling.Ignore)]
        public Audio Audio { get; set; }

        /// <summary>
        /// The content stream, if the item represents a file.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public Stream Content { get; set; }

        /// <summary>
        /// Identity of the user, device, and application which created the item. Read-only.
        /// </summary>
        /// <value>The created by.</value>
        [JsonProperty("createdBy")]
        public IdentitySet CreatedBy { get; set; }

        /// <summary>
        /// Date and time of item creation. Read-only.
        /// </summary>
        /// <value>The created date time.</value>
        [JsonProperty("createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Information about the deleted state of the item. Read-only.
        /// </summary>
        /// <value>The CT ag.</value>
        [JsonProperty("deleted", NullValueHandling = NullValueHandling.Ignore)]
        public Deleted Deleted { get; set; }

        /// <summary>
        /// Provides a user-visible description of the item. Read-write. Only on OneDrive Personal
        /// </summary>
        /// <value>The ET ag.</value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// eTag for the entire item (metadata + content). Read-only.
        /// </summary>
        /// <value>The ET ag.</value>
        [JsonProperty("eTag")]
        public string ETag { get; set; }

        /// <summary>
        /// File metadata, if the item is a file. Read-only.
        /// </summary>
        /// <value>The file.</value>
        [JsonProperty("file", NullValueHandling = NullValueHandling.Ignore)]
        public File File { get; set; }

        /// <summary>
        /// File system information on client. Read-write.  https://docs.microsoft.com/en-us/graph/api/resources/filesysteminfo?view=graph-rest-1.0
        /// </summary>
        /// <value>The file system info.</value>
        [JsonProperty("fileSystemInfo", NullValueHandling = NullValueHandling.Ignore)]
        public FileSystemInfo FileSystemInfo { get; set; }

        /// <summary>
        /// Folder metadata, if the item is a folder. Read-only.
        /// </summary>
        /// <value>The folder.</value>
        [JsonProperty("folder", NullValueHandling = NullValueHandling.Ignore)]
        public Folder Folder { get; set; }

        /// <summary>
        /// The unique identifier of the item within the Drive. Read-only.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Image metadata, if the item is an image. Read-only.
        /// </summary>
        /// <value>The image.</value>
        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public Image Image { get; set; }

        /// <summary>
        /// Date and time the item was last modified. Read-only.
        /// </summary>
        /// <value>The last modified date time.</value>
        [JsonProperty("lastModifiedDateTime")]
        public DateTimeOffset LastModifiedDateTime { get; set; }

        /// <summary>
        /// Identity of the user, device, and application which last modified the item. Read-only.
        /// </summary>
        /// <value>The last modified by.</value>
        [JsonProperty("lastModifiedBy")]
        public IdentitySet LastModifiedBy { get; set; }

        /// <summary>
        /// Location metadata, if the item has location data. Read-only.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public GeoCoordinates Location { get; set; }

        /// <summary>
        /// A URL that can be used to download this file's content. Authentication is not required with this URL. Read-only.
        /// </summary>
        /// <value>The microsoft graph download URL.</value>
        [JsonProperty("@microsoft.graph.downloadUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri MicrosoftGraphDownloadUrl { get; set; }

        /// <summary>
        /// The name of the item (filename and extension). Read-write.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// If present, indicates that this item is a package instead of a folder or file. Packages are treated like files in some contexts and folders in others. Read-only.
        /// </summary>
        /// <value>The package.</value>
        [JsonProperty("package", NullValueHandling = NullValueHandling.Ignore)]
        public Package Package { get; set; }

        /// <summary>
        /// Parent information, if the item has a parent. Read-write.
        /// </summary>
        /// <value>The parent reference.</value>
        [JsonProperty("parentReference", NullValueHandling = NullValueHandling.Ignore)]
        public ItemReference ParentReference { get; set; }

        /// <summary>
        /// Photo metadata, if the item is a photo. Read-only.
        /// </summary>
        /// <value>The photo.</value>
        [JsonProperty("photo", NullValueHandling = NullValueHandling.Ignore)]
        public Photo Photo { get; set; }

        /// <summary>
        /// Indicates that the item has been shared with others and provides information about the shared state of the item. Read-only.
        /// </summary>
        /// <value>The shared.</value>
        [JsonProperty("shared", NullValueHandling = NullValueHandling.Ignore)]
        public Shared Shared { get; set; }

        /// <summary>
        /// Size of the item in bytes. Read-only.
        /// </summary>
        /// <value>The size.</value>
        [JsonProperty("size")]
        public long Size { get; set; }

        /// <summary>
        /// If the current item is also available as a special folder, this facet is returned. Read-only.
        /// </summary>
        /// <value>The special folder.</value>
        [JsonProperty("specialFolder", NullValueHandling = NullValueHandling.Ignore)]
        public SpecialFolder SpecialFolder { get; set; }

        /// <summary>
        /// URL that displays the resource in the browser. Read-only.
        /// </summary>
        /// <value>The web URL.</value>
        [JsonProperty("webUrl")]
        public Uri WebUrl { get; set; }

        /// <summary>
        /// Video metadata, if the item is a video. Read-only.
        /// </summary>
        /// <value>The video.</value>
        [JsonProperty("video", NullValueHandling = NullValueHandling.Ignore)]
        public Video Video { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/audio?view=graph-rest-1.0
    /// 
    /// The Audio resource groups audio-related properties on an item into a single structure.
    /// If a DriveItem has a non-null audio facet, the item represents an audio file.The properties of the Audio resource are populated by extracting metadata from the file.
    /// </summary>
    public class Audio
    {
        /// <summary>
        /// The title of the album for this audio file.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("album")]
        public string Album { get; set; }

        /// <summary>
        /// The artist named on the album for the audio file.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("albumArtist")]
        public string AlbumArtist { get; set; }

        /// <summary>
        /// The performing artist for the audio file.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// Bit rate of the video in bits per second.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("bitrate")]
        public long Bitrate { get; set; }

        /// <summary>
        /// The name of the composer of the audio file.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("composers")]
        public string Composers { get; set; }

        /// <summary>
        /// Copyright information for the audio file.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// The number of the disc this audio file came from.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("disc")]
        public short Disc { get; set; }

        /// <summary>
        /// The total number of discs in this album.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("discCount")]
        public short DiscCount { get; set; }

        /// <summary>
        /// Duration of the file in milliseconds.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("duration")]
        public long Duration { get; set; }

        /// <summary>
        /// The genre of this audio file.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Indicates if the file is protected with digital rights management.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("hasDrm")]
        public bool HasDRM { get; set; }

        /// <summary>
        /// Indicates if the file is encoded with a variable bitrate.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("isVariableBitrate")]
        public bool IsVariableBitrate { get; set; }

        /// <summary>
        /// The title of the audio file.
        /// </summary>
        /// <value>The height.</value>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The number of the track on the original disc for this audio file.
        /// </summary>
        /// <value>The height.</value>
        [JsonProperty("track")]
        public int Track { get; set; }

        /// <summary>
        /// The total number of tracks on the original disc for this audio file.
        /// </summary>
        /// <value>The height.</value>
        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        /// <summary>
        /// The year the audio file was recorded.
        /// </summary>
        /// <value>The width.</value>
        [JsonProperty("year")]
        public int Year { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/deleted?view=graph-rest-1.0
    /// 
    /// The Deleted resource indicates that the item has been deleted. In this version of the API, the presence (non-null) of the resource value indicates that the file was deleted. A null (or missing) value indicates that the file is not deleted.
    /// See view changes for an item for more information on tracking changes and finding deleted items.
    /// </summary>
    public class Deleted
    {
        /// <summary>
        /// Represents the state of the deleted item.
        /// </summary>
        /// <value>The child count.</value>
        [JsonProperty("state")]
        public string State { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/file?view=graph-rest-1.0
    /// 
    /// The File resource groups file-related data items into a single structure.  If a DriveItem has a non-null file facet, the item represents an file. In addition to other properties, files have a content relationship which contains the byte stream of the file. https://docs.microsoft.com/en-us/graph/api/resources/file?view=graph-rest-1.0
    /// </summary>
    public class File
    {
        /// <summary>
        /// The MIME type for the file. This is determined by logic on the server and might not be the value provided when the file was uploaded. Read-only.
        /// </summary>
        /// <value>The type of the MIME.</value>
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// Hashes of the file's binary content, if available. Read-only.
        /// </summary>
        /// <value>The hashes.</value>
        [JsonProperty("hashes")]
        public Hashes Hashes { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/filesysteminfo?view=graph-rest-1.0
    /// 
    /// The FileSystemInfo resource contains properties that are reported by the device's local file system for the local version of an item. This facet can be used to specify the last modified date or created date of the item as it was on the local device.
    /// It is available on the fileSystemInfo property of driveItem resources.
    /// </summary>
    public class FileSystemInfo
    {
        /// <summary>
        /// The UTC date and time the file was created on a client.
        /// </summary>
        /// <value>The created date time.</value>
        [JsonProperty("createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// The UTC date and time the file was last modified on a client.
        /// </summary>
        /// <value>The last modified date time.</value>
        [JsonProperty("lastModifiedDateTime")]
        public DateTimeOffset LastModifiedDateTime { get; set; }

        /// <summary>
        /// The UTC date and time the file was last accessed. Available for the recent file list only.
        /// </summary>
        /// <value>The last modified date time.</value>
        [JsonProperty("lastAccessedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastAccessedDateTime { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/folder?view=graph-rest-1.0
    /// 
    /// The Folder resource groups folder-related data on an item into a single structure. DriveItems with a non-null folder facet are containers for other DriveItems.  https://docs.microsoft.com/en-us/graph/api/resources/folder?view=graph-rest-1.0
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// Number of children contained immediately within this container.
        /// </summary>
        /// <value>The child count.</value>
        [JsonProperty("childCount")]
        public long ChildCount { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/geocoordinates?view=graph-rest-1.0
    /// 
    /// The GeoCoordinates resource provides geographic coordinates and elevation of a location based on metadata contained within the file.If a DriveItem has a non-null location facet, the item represents a file with a known location assocaited with it
    /// </summary>
    public class GeoCoordinates
    {
        /// <summary>
        /// Optional. The altitude (height), in feet, above sea level for the item. Read-only.
        /// </summary>
        /// <value>The child count.</value>
        [JsonProperty("altitude")]
        public double Altitude { get; set; }

        /// <summary>
        /// Optional. The latitude, in decimal, for the item. Read-only.
        /// </summary>
        /// <value>The child count.</value>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Optional. The longitude, in decimal, for the item. Read-only
        /// </summary>
        /// <value>The child count.</value>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/hashes?view=graph-rest-1.0
    /// 
    /// The Hashes resource groups available hashes into a single structure for an item.
    /// Note: Not all services provide a value for all hash properties listed.
    /// </summary>
    public class Hashes
    {
        /// <summary>
        /// A proprietary hash of the file that can be used to determine if the contents of the file have changed (if available). Read-only.
        /// </summary>
        /// <value>The quick xor hash.</value>
        [JsonProperty("quickXorHash")]
        public string QuickXorHash { get; set; }

        /// <summary>
        /// The CRC32 value of the file in little endian (if available). Read-only.
        /// </summary>
        /// <value>The quick xor hash.</value>
        [JsonProperty("crc32Hash")]
        public string CRC32Hash { get; set; }

        /// <summary>
        /// SHA1 hash for the contents of the file (if available). Read-only.
        /// </summary>
        /// <value>The quick xor hash.</value>
        [JsonProperty("sha1Hash")]
        public string Sha1Hash { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/identityset?view=graph-rest-1.0
    /// 
    /// The IdentitySet resource is a keyed collection of identity resources. It is used to represent a set of identities associated with various events for an item, such as created by or last modified by.
    /// </summary>
    public class IdentitySet
    {
        /// <summary>
        /// Optional. The application associated with this action.
        /// </summary>
        /// <value>The application.</value>
        [JsonProperty("application", NullValueHandling = NullValueHandling.Ignore)]
        public Identity Application { get; set; }

        /// <summary>
        /// Optional. The device associated with this action.
        /// </summary>
        /// <value>The user.</value>
        [JsonProperty("device", NullValueHandling = NullValueHandling.Ignore)]
        public Identity device { get; set; }

        /// <summary>
        /// Optional. The user associated with this action.
        /// </summary>
        /// <value>The user.</value>
        [JsonProperty("user")]
        public User User { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/identity?view=graph-rest-1.0
    /// 
    /// The Identity resource represents an identity of an actor. For example, an actor can be a user, device, or application.
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Unique identifier for the identity.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonIgnore]
        public Guid GuidId => Guid.Parse(Id);

        /// <summary>
        /// The identity's display name. Note that this may not always be available or up to date. For example, if a user changes their display name, the API may show the new value in a future response, but the items associated with the user won't show up as having changed when using delta(https://docs.microsoft.com/en-us/graph/api/driveitem-delta?view=graph-rest-1.0&tabs=cs).
        /// </summary>
        /// <value>The display name.</value>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/image?view=graph-rest-1.0
    /// 
    /// The Image resource groups image-related properties into a single structure. If a DriveItem has a non-null image facet, the item represents a bitmap image.
    /// Note: If the service is unable to determine the width and height of the image, the Image resource may be empty.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Optional. Height of the image, in pixels. Read-only.
        /// </summary>
        /// <value>The height.</value>
        [JsonProperty("height")]
        public long Height { get; set; }

        /// <summary>
        /// Optional. Width of the image, in pixels. Read-only.
        /// </summary>
        /// <value>The width.</value>
        [JsonProperty("width")]
        public long Width { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/itemreference?view=graph-rest-1.0
    /// 
    /// The ItemReference resource provides information necessary to address a DriveItem via the API.
    /// </summary>
    public class ItemReference
    {
        /// <summary>
        /// Unique identifier of the drive instance that contains the item. Read-only.
        /// </summary>
        /// <value>The drive identifier.</value>
        [JsonProperty("driveId")]
        public string DriveId { get; set; }

        /// <summary>
        /// Identifies the type of drive. See drive(https://docs.microsoft.com/en-us/graph/api/resources/drive?view=graph-rest-1.0) resource for values.
        /// </summary>
        /// <value>The type of the drive.</value>
        [JsonProperty("driveType")]
        public string DriveType { get; set; }

        /// <summary>
        /// Unique identifier of the item in the drive. Read-only.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the item being referenced. Read-only.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Path that can be used to navigate to the item. Read-only.
        /// </summary>
        /// <value>The path.</value>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// A unique identifier for a shared resource that can be accessed via the Shares(https://docs.microsoft.com/en-us/graph/api/shares-get?view=graph-rest-1.0&tabs=cs) API.
        /// </summary>
        /// <value>The shared identifier.</value>
        [JsonProperty("sharedId")]
        public string SharedId { get; set; }

        /// <summary>
        /// Returns identifiers useful for SharePoint REST compatibility. Read-only.
        /// </summary>
        /// <value>The share point identifiers.</value>
        [JsonProperty("sharepointIds", NullValueHandling = NullValueHandling.Ignore)]
        public SharePointIds SharePointIds { get; set; }

    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/package?view=graph-rest-1.0
    /// 
    /// The Package resource indicates that a DriveItem is the top level item in a "package" or a collection of items that should be treated as a collection instead of individual items.
    /// An example of a package is a OneNote notebook.While the notebook is made up of files and folders that represent the contents of the notebook, the top level item that represents the notebook has a package facet to indicate to clients that this is a collection of data that should be treated special.
    /// DriveItems with the package facet do not include a folder or file facet but are conceptually similar to an item with a folder facet.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// A string indicating the type of package. While oneNote is the only currently defined value, you should expect other package types to be returned and handle them accordingly.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/photo?view=graph-rest-1.0
    /// 
    /// The photo resource provides photo and camera properties, for example, EXIF metadata, on a driveItem.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Represents the date and time the photo was taken. Read-only.
        /// </summary>
        /// <value>The taken date time.</value>
        [JsonProperty("takenDateTime")]
        public DateTimeOffset TakenDateTime { get; set; }

        /// <summary>
        /// Camera manufacturer. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("cameraMake")]
        public string CameraMake { get; set; }

        /// <summary>
        /// Camera model. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("cameraModel")]
        public string CameraModel { get; set; }

        /// <summary>
        /// The F-stop value from the camera. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("fNumber")]
        public double FNumber { get; set; }

        /// <summary>
        /// The denominator for the exposure time fraction from the camera. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("exposureDenominator")]
        public double ExposureDenominator { get; set; }

        /// <summary>
        /// The numerator for the exposure time fraction from the camera. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("exposureNumerator")]
        public double ExposureNumerator { get; set; }

        /// <summary>
        /// The focal length from the camera. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("focalLength")]
        public double FocalLength { get; set; }

        /// <summary>
        /// The ISO value from the camera. Read-only.
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("iso")]
        public int ISO { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/shared?view=graph-rest-1.0
    /// 
    /// The Shared resource indicates a DriveItem has been shared with others. The resource includes information about how the item is shared.
    /// If a Driveitem has a non-null shared facet, the item has been shared.
    /// </summary>
    public class Shared
    {
        /// <summary>
        /// The identity of the owner of the shared item. Read-only.
        /// </summary>
        /// <value>The owner.</value>
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public IdentitySet Owner { get; set; }

        /// <summary>
        /// The identity of the user who shared the item. Read-only.
        /// </summary>
        /// <value>The shared by.</value>
        [JsonProperty("sharedBy", NullValueHandling = NullValueHandling.Ignore)]
        public IdentitySet SharedBy { get; set; }

        /// <summary>
        /// Indicates the scope of how the item is shared: anonymous, organization, or users. Read-only.
        /// </summary>
        /// <value>The scope.</value>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// The UTC date and time when the item was shared. Read-only.
        /// </summary>
        /// <value>The last modified date time.</value>
        [JsonProperty("sharedDateTime")]
        public DateTimeOffset SharedDateTime { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/sharepointids?view=graph-rest-1.0
    /// 
    /// The SharePointIds resource groups the various identifiers for an item stored in a SharePoint site or OneDrive for Business into a single structure.
    /// Note: items returned from OneDrive personal will not include a SharePointIds facet.
    /// </summary>
    public class SharePointIds
    {
        /// <summary>
        /// The unique identifier (guid) for the item's list in SharePoint.
        /// </summary>
        /// <value>The list identifier.</value>
        [JsonProperty("listId")]
        public string ListId { get; set; }

        /// <summary>
        /// An integer identifier for the item within the containing list.
        /// </summary>
        /// <value>The list item identifier.</value>
        [JsonProperty("listItemId")]
        public string ListItemId { get; set; }

        // <summary>
        /// The unique identifier (guid) for the item within OneDrive for Business or a SharePoint site.
        /// </summary>
        /// <value>The list item unique identifier.</value>/
        [JsonProperty("listItemUniqueId")]
        public string ListItemUniqueId { get; set; }

        /// <summary>
        /// The unique identifier (guid) for the item's site collection (SPSite).
        /// </summary>
        /// <value>The site identifier.</value>
        [JsonProperty("siteId")]
        public string SiteId { get; set; }

        /// <summary>
        /// The SharePoint URL for the site that contains the item.
        /// </summary>
        /// <value>The site URL.</value>
        [JsonProperty("siteUrl")]
        public string SiteUrl { get; set; }

        /// <summary>
        /// The unique identifier (guid) for the item's site (SPWeb).
        /// </summary>
        /// <value>The web identifier.</value>
        [JsonProperty("webId")]
        public string WebId { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/specialfolder?view=graph-rest-1.0
    /// 
    /// The SpecialFolder resource groups special folder-related data items into a single structure.
    /// If a DriveItem has a non-null specialFolder facet, the item represents a special(named) folder.Special folders can be accessed directly via the special folders collection.
    /// Special folders provide simple aliases to access well-known folders without the need to look up the folder by path(which would require localization), or reference the folder with an ID.If a special folder is renamed or moved to another location within the drive, this syntax will continue to return that folder.
    /// Special folders are automatically created the first time an application attempts to write to one, if it doesn't already exist. If a user deletes one, it is recreated when written to again.
    /// Note: If your app has only requested Files.Read scope and requests a special folder that doesn't exist, the response will be a 403 Forbidden error.
    /// </summary>
    public class SpecialFolder
    {
        /// Here are the special folders available in OneDrive Personal and OneDrive for Business.
        public static string FolderAppRoot = "approot";
        public static string FolderCameraRoll = "cameraroll";
        public static string FolderDocuments = "documents";
        public static string FolderMusic = "music";
        public static string FolderPhotos = "photos";
        /// <summary>
        /// The unique identifier for this item in the /drive/special collection
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("displayName")]
        public string FullName { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/video?view=graph-rest-1.0
    /// 
    /// The Video resource groups video-related data items into a single structure.
    /// If a DriveItem has a non-null video facet, the item represents a video file.The properties of the Video resource are populated by extracting metadata from the file.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Number of audio bits per sample.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("audioBitsPerSample")]
        public int AudioBitsPerSample { get; set; }

        /// <summary>
        /// Number of audio bits per sample.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("audioChannels")]
        public int AudioChannels { get; set; }

        /// <summary>
        /// Name of the audio format (AAC, MP3, etc.).
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("audioFormat")]
        public string AudioFormat { get; set; }

        /// <summary>
        /// Number of audio samples per second.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("audioSamplesPerSecond")]
        public int AudioSamplesPerSecond { get; set; }

        /// <summary>
        /// Bit rate of the video in bits per second.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("bitrate")]
        public long Bitrate { get; set; }

        /// <summary>
        /// Duration of the file in milliseconds.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("duration")]
        public long Duration { get; set; }

        /// <summary>
        /// "Four character code" name of the video format.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("fourCC")]
        public string FourCC { get; set; }

        /// <summary>
        /// "Four character code" name of the video format.
        /// </summary>
        /// <value>The audio bits per sample.</value>
        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }

        /// <summary>
        /// Optional. Height of the image, in pixels. Read-only.
        /// </summary>
        /// <value>The height.</value>
        [JsonProperty("height")]
        public long Height { get; set; }

        /// <summary>
        /// Optional. Width of the image, in pixels. Read-only.
        /// </summary>
        /// <value>The width.</value>
        [JsonProperty("width")]
        public long Width { get; set; }
    }
}
