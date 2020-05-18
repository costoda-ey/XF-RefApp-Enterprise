using System;
namespace Mobile.RefApp.Lib.Keychain
{
    public class SecRecord
    {
        public string AccessGroup { get; set; }
        public string Account { get; set; }
        public string ApplicationLabel { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public string SecurityDomain { get; set; }
        public string Subject { get; set; }
        public string Service { get; set; }
    }
}
