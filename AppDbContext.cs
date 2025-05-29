using Microsoft.EntityFrameworkCore;
using ComputerClubApp.Models;

namespace ComputerClubApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=compclub;Username=postgres;Password=12345");
        }
    }
}
