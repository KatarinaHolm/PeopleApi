namespace PeopleApi.Models
{
    public class Link
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int PersonInterestId_FK { get; set; }

        public PersonInterest PersonInterest { get; set; }

    }
}
