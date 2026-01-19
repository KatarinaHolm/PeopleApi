namespace PeopleApi.Models
{
    public class Link
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int PersonId_FK { get; set; }

        public Person Person { get; set; }

        public int InterestId_FK { get; set; }

        public Interest Interest { get; set; }

    }
}
