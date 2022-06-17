using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBLibrary
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; } = null!;
        private IConfigurationRoot? _configuration;
        private string InventoryManager = nameof(InventoryManager);

        public InventoryDbContext()
        {

        }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                _configuration = builder.Build();

                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(InventoryManager));
            }
        }
    }
}
