using Microsoft.EntityFrameworkCore;
using PeopleApi.Models;

namespace PeopleApi.Data
{
    public class PeopleApiDbContext : DbContext
    {
        public PeopleApiDbContext(DbContextOptions<PeopleApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Link> Links { get; set; }

        public  DbSet<PersonInterest> MyProperty { get; set; }
    }
}
