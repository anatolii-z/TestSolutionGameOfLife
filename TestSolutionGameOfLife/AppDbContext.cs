using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestSolutionGameOfLife
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(App.Config.GetConnectionString("DbConnection"));
        }
    }
}
