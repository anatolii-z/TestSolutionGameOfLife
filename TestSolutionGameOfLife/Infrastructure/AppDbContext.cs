using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestSolutionGameOfLife
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(App.Config.GetConnectionString("DbConnection"));
        }
    }
}
