namespace NASASerwerAPI.Models
{
    public class MissionModel
    {
        public string Institution { get; set; }
        public List<string> Roles { get; set; }
        public Person person { get; set; }

        public class Person
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
            public string Phone { get; set; }
            public VersionInfo versionInfo { get; set; }
        }
        public class VersionInfo
        {
            public string DocumentKey { get; set; }
            public int Version { get; set; }
            public bool Deleted { get; set; }
        }
    }
}
