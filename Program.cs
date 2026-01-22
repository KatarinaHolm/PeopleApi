
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PeopleApi.Data;
using PeopleApi.DTOs;
using PeopleApi.Models;
using Scalar.AspNetCore;
using System;

namespace PeopleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<PeopleApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();  
            
            //Get all persons
            app.MapGet("/persons", async (PeopleApiDbContext context) =>
            {
                var Persons = await context.Persons.ToListAsync();

                if (!Persons.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Persons);

            });

            // It gets wrong when running it, makes circular reference.
            //app.MapGet("/interests/{id}", async (PeopleApiDbContext context, int id) =>
            //{
            //    var person = await context.Persons
            //        .Include(p => p.PersonInterests)
            //        .ThenInclude(pi => pi.Interest)
            //        .FirstOrDefaultAsync(p => p.Id == id);

            //    var interests = person.PersonInterests
            //        .Select(pi => pi.Interest)
            //        .ToList();

            //    return Results.Ok(interests);
            //});

            //Get all interests for a specific person
            app.MapGet("/persons/{id}/interests", async (PeopleApiDbContext context, int id) =>
            {
                var PersonInterests = await context.PersonsInterests
                    .Where(pi => pi.PersonId == id)
                    .Select(pi => new InterestDto
                    {
                        Id = pi.Interest.Id,
                        Title = pi.Interest.Title,
                        Description = pi.Interest.Description
                    })
                    .ToListAsync();

                if (!PersonInterests.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(PersonInterests);
            });

            //Get all links for a specific person
            app.MapGet("/persons/{id}/links", async (PeopleApiDbContext context, int id) =>
            {
                var PersonLinks = await context.PersonsInterests
                    .Where(pi => pi.PersonId == id)
                    .SelectMany(pi => pi.Links.Select(l => l.Url))
                    .ToListAsync();

                if (!PersonLinks.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(PersonLinks);
            });


            //Connect a person to a new interest
            app.MapPost("/persons/{personId}/interests/{interestId}", async (PeopleApiDbContext context, int personId, int interestId) =>
            {
                var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == personId);

                var interest = await context.Interests.FirstOrDefaultAsync(i => i.Id == interestId);

                if (person == null || interest == null )
                {
                    return Results.NotFound("Person eller intresse saknas.");
                }

                var personInterest = await context.PersonsInterests
                    .FirstOrDefaultAsync(pi => pi.PersonId == personId && pi.InterestId == interestId);

                if (personInterest != null)
                {
                    return Results.Conflict();
                }

                var newPersonInterest = new PersonInterest
                {
                    PersonId = personId,
                    InterestId = interestId
                };

                context.PersonsInterests.Add(newPersonInterest);
                await context.SaveChangesAsync();

                return Results.Created($"/persons/{personId}/interests/{interestId}", newPersonInterest);
            });


            //Add new links to a specific person and a specific interest
            app.MapPost("/persons/{personId}/interests/{interestId}/links", async (PeopleApiDbContext context, int personId, int interestId, LinkDto dto) =>
            {
                var personInterest = await context.PersonsInterests
                    .FirstOrDefaultAsync(pi => pi.PersonId == personId && pi.InterestId == interestId);

                if (personInterest == null)
                {
                    return Results.NotFound("Person eller intresse saknas.");
                }

                var link = new Link
                {
                    Url = dto.Url,
                    PersonInterestId = personInterest.Id
                };

                context.Links.Add(link);
                await context.SaveChangesAsync();

                Results.Created($"/persons/{personId}/interests/{interestId}/links", link);
            });

            app.Run();
        }
    }
}
