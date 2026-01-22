using Microsoft.EntityFrameworkCore;
using PeopleApi.Models;
using System;
using System.Runtime.ConstrainedExecution;

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

        public DbSet<PersonInterest> PersonsInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                        Id = 1,
                        FirstName = "Mohammed",
                        LastName = "Malik",
                        PhoneNr = "072-789 23 41"
                },
                
                new Person
                {
                    Id = 2,
                    FirstName = "Sara",
                    LastName = "Wennergren",
                    PhoneNr = "076-346 44 81"
                },

                new Person
                {
                    Id = 3,
                    FirstName = "Lydia",
                    LastName = "Abou",
                    PhoneNr = "073-339 15 47"
                }
                );

            modelBuilder.Entity<Interest>().HasData(
                new Interest
                {
                    Id = 1,
                    Title = "Körsång",
                    Description = "Körsång är en form av vokalmusik där en grupp människor sjunger tillsammans, oftast under ledning av en dirigent. Det är en kollektiv konstform som bygger på samspel, gemenskap och harmonisering av röster."
                },

                new Interest
                {
                    Id = 2,
                    Title = "Träning",
                    Description = "Träning är fysisk aktivitet med mål att förbättra styrka, kondition, rörlighet eller färdigheter genom regelbunden ansträngning, där kroppen anpassar sig genom att bygga upp muskler, stärka skelettet och förbättra hjärnans funktioner för att hantera ökad stress. " +
                    "Det involverar planering med specifika mål, val av övningar (som knäböj, bänkpress), rätt intensitet och återhämtning, ofta rekommenderat minst två gånger i veckan för styrka, med variation och progression för bästa resultat. "
                },

                new Interest
                {
                    Id = 3,
                    Title = "Programmering",
                    Description = "Programmering är konsten att skriva instruktioner (kod) i ett programmeringsspråk som en dator eller annan apparatur kan förstå för att utföra specifika uppgifter, lösa problem, skapa appar, webbplatser eller automatisera processer; " +
                    "det är i grunden logiskt tänkande och att bryta ner en uppgift i små, exakta steg (en algoritm) som sedan översätts till maskinkod. Processen inkluderar analys, planering, kodning, testning och felsökning. "
                },
                new Interest
                {
                    Id = 4,
                    Title = "Matlagning",
                    Description = "Matlagning är konsten, vetenskapen och hantverket att använda värme för att omvandla livsmedelsråvaror till färdig mat.Syftet är att göra maten mer välsmakande, lättsmält, näringsrik eller säker att äta. " +
                    "Det innefattar en rad tekniker, från grundläggande förberedelser till avancerade metoder som kräver precision."
                }                

                );

            modelBuilder.Entity<PersonInterest>().HasData(
                new PersonInterest
                {
                    Id = 1,
                    PersonId = 1,
                    InterestId = 1
                },          
                   
                new PersonInterest
                {
                    Id = 2,
                    PersonId = 1,
                    InterestId = 2
                },

                new PersonInterest
                {
                    Id = 3,
                    PersonId = 2,
                    InterestId = 3
                },

                new PersonInterest
                {
                    Id = 4,
                    PersonId = 2,
                    InterestId = 1
                },

                new PersonInterest
                {
                    Id = 5,
                    PersonId = 3,
                    InterestId = 4
                },

                new PersonInterest
                {
                    Id = 6,
                    PersonId = 3,
                    InterestId = 3
                }
                );

            modelBuilder.Entity<Link>().HasData(
                new Link
                {
                    Id = 1,

                    Url = "https://www.svenskakyrkan.se/sofia/sofia-nova",

                    PersonInterestId = 1
                },

                new Link
                {
                    Id = 2,

                    Url = "https://www.sverigeskorforbund.se/",

                    PersonInterestId = 1

                },

                new Link
                {
                    Id = 3,

                    Url = "https://www.sats.se/",

                    PersonInterestId = 2

                },

                new Link
                {
                    Id = 4,

                    Url = "https://chasacademy.se/",

                    PersonInterestId = 3

                },

                new Link
                {
                    Id = 5,

                    Url = "https://hjkk.se/",

                    PersonInterestId = 4

                },

                new Link
                {
                    Id = 6,

                    Url = "https://www.koket.se/",

                    PersonInterestId = 5

                },

                new Link
                {
                    Id = 7,

                    Url = "https://csharpskolan.se/",

                    PersonInterestId = 6

                },
                new Link
                {
                    Id = 8,

                    Url = "https://learn.microsoft.com/en-us/dotnet/csharp/",

                    PersonInterestId = 6
                }

                );
        }
    }
}
