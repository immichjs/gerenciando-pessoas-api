using GerenciandoPessoas.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciandoPessoas.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Pessoa>()
                .ToTable("Pessoas");

            model.Entity<Pessoa>()
                .Property(p => p.Name).HasMaxLength(250).IsRequired();

            model.Entity<Pessoa>()
                .Property(p => p.Cpf).HasMaxLength(250).IsRequired();

            model.Entity<Pessoa>()
                .Property(p => p.Email).HasMaxLength(250).IsRequired();
        }
    }
}
