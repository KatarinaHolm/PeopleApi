namespace PeopleApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNr { get; set; }

        public ICollection<PersonInterest> PersonInterests { get; set; } = new List<PersonInterest>();

    }
}
