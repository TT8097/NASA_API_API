using System.Text.Json.Serialization;

namespace NASASerwerAPI.Models
{
    public class MissionModelUser
    {
        public string Institution { get; set; }
        public Person person { get; set; }

        public class Person
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }

        }
    }
}
