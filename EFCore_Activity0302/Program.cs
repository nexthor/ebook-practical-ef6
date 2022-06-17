using EFCore_DBLibrary;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace EFCore_Activity0302
{
    internal class Program
    {
        private static IConfigurationRoot? _configuration;
        private static DbContextOptionsBuilder<InventoryDbContext>? _optionsBuilder;
        private static string InventoryManager = nameof(InventoryManager);

        static void Main(string[] args)
        {
            BuildOptions();
            EnsureItems();

            ListItems();

            Console.ReadKey();
        }

        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString(InventoryManager));
        }

        private static void EnsureItems()
        {
            EnsureItem("Batman Begins");
            EnsureItem("The Dark Knight");
            EnsureItem("The Batman");
            EnsureItem("Batman Returns");
        }

        private static void EnsureItem(string name)
        {
            if (_optionsBuilder != null)
            {
                using var db = new InventoryDbContext(_optionsBuilder.Options);
                var itemExists = db.Items.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

                if (itemExists == null)
                {
                    var item = new Item()
                    {
                        Name = name
                    };

                    db.Items.Add(item);
                    db.SaveChanges();
                }
            }
        }

        private static void ListItems()
        {
            if (_optionsBuilder != null)
            {
                using var db = new InventoryDbContext(_optionsBuilder.Options);
                var items = db.Items.ToList().Take(10);

                foreach (var item in items)
                    Console.WriteLine($"item: {item.Name}");
            }
        }
    }
}