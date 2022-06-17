using EFCore_DBLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace EFCore_Activity0201
{
    internal class Program
    {
        private static IConfigurationRoot? _configuration;
        private static string? AdventureWorks = nameof(AdventureWorks);
        private static DbContextOptionsBuilder<AdventureWorksContext>? _optionsBuilder;

        static void Main(string[] args)
        {
            BuildConfiguration();
            BuildOptions();
            ListPeople();

            Console.ReadKey();
        }

        static void BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        static void BuildOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString(AdventureWorks));
        }

        static void ListPeople()
        {
            using var db = new AdventureWorksContext(_optionsBuilder?.Options);
            var people = db.People.OrderByDescending(x => x.LastName).Take(20);

            foreach(var person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}");
            }
        }
    }
}