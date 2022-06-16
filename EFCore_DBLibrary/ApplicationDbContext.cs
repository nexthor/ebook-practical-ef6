using Microsoft.EntityFrameworkCore;

namespace EFCore_DBLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}