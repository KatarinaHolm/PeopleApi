namespace PeopleApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string phoneNr { get; set; }

        public List<Link> Links { get; set; }
    }
}
