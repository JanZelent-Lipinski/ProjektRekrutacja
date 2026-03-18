using System.Data.Entity;
using ProjektRekrutacja.Models;

namespace ProjektRekrutacja.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=SubiektDb")
        {
            Database.SetInitializer<AppDbContext>(null);
        }
        public DbSet<Asortyment> Asortymenty { get; set; }
        public DbSet<StanMagazynowy> StanyMagazynowe { get; set; }
        public DbSet<JednostkaMiary> JednostkiMiar { get; set; }
        public DbSet<JednostkaMiaryAsortymentu> JednostkiMiarAsortymentow { get; set; }
        public DbSet<KodyKreskowe> KodyKreskowe { get; set; }

    }
}