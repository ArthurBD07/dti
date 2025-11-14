using LeadManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Api.Data
{
    public class LeadDbContext : DbContext
    {
        public LeadDbContext(DbContextOptions<LeadDbContext> options) : base(options)
        {
        }

        public DbSet<Lead> Leads => Set<Lead>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Diz pro EF que o ID NÃO é gerado automaticamente,
            // então podemos inserir 5577421, 5588872 etc.
            modelBuilder.Entity<Lead>()
                .Property(l => l.Id)
                .ValueGeneratedNever();

                DataSeeder.Seed(modelBuilder);

        }
    }
}
