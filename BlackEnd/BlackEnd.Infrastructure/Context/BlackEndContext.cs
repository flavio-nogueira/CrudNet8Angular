using BlackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlackEnd.Infrastructure.Context
{
    public class BlackEndContext : DbContext
    {
        public BlackEndContext(DbContextOptions<BlackEndContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.ClienteMap());
            base.OnModelCreating(modelBuilder);

        }
    }
}
