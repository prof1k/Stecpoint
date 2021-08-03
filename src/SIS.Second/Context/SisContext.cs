namespace SIS.Second.Context
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;

    using SIS.Domain.Model.Entity;

    public class SisContext : DbContext
    {
        public SisContext()
        {
        }

        public SisContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}