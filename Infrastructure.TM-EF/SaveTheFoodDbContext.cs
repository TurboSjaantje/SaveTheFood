using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TM_EF
{
    public class SaveTheFoodDbContext : DbContext
    {
        public SaveTheFoodDbContext(DbContextOptions<SaveTheFoodDbContext> options) : base(options) { }

        public DbSet<Kantine> Kantines { get; set; }
        public DbSet<Product> Producten { get; set; }
        public DbSet<Student> Studenten { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Pakket> Pakketten { get; set; }
    }
}
