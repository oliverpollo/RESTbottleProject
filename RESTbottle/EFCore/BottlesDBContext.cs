using Microsoft.EntityFrameworkCore;

namespace RESTbottle.EFCore
{
    public class BottlesDBContext : DbContext
    {
        public BottlesDBContext(DbContextOptions<BottlesDBContext> options) : base(options)
        {

        }

        public DbSet<Models.Bottle> Bottles { get; set; }
    }
}
