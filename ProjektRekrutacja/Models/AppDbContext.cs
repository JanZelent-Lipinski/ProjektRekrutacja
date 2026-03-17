using System.Data.Entity;
using ProjektRekrutacja.Models;

namespace ProjektRekrutacja.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=SubiektDb")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}