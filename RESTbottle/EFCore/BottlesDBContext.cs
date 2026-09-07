using Microsoft.EntityFrameworkCore;
using RESTbottle.Models;

namespace RESTbottle.EFCore
{
    public class BottlesDBContext : DbContext
    {
        public DbSet<Bottle> Bottles { get; set; }

        public BottlesDBContext(
            DbContextOptions<BottlesDBContext> options)
            : base(options)
        {
        }
    }
}