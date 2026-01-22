
using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.DTOs;
using Scalar.AspNetCore;

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
            
            app.MapGet("/persons", async (PeopleApiDbContext context) =>
            {
                var persons = await context.Persons.ToListAsync();

                return Results.Ok(persons);

            });
            // Blir fel när API:et kallas
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

            app.MapGet("/persons/{id}/interests", async (PeopleApiDbContext context, int id) =>
            {
                var personInterests = await context.PersonsInterests
                    .Where(pi => pi.PersonId == id)
                    .Select(pi => new InterestDto
                    {
                        Id = pi.Interest.Id,
                        Title = pi.Interest.Title,
                        Description = pi.Interest.Description
                    })
                    .ToListAsync();                  

                return Results.Ok(personInterests);
            });

            app.Run();
        }
    }
}
