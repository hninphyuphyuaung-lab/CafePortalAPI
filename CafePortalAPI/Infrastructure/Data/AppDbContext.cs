using CafePortalAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CafePortalAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<DbContext> options) : base(options) { }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Load the configuration (assuming appsettings.json or environment variable)
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json") 
                    .Build();

                // Get the connection string from the configuration
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                // Configure the DbContext to use SQL Server
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cafe>().HasKey(c => c.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            // Additional configuration for relationships and properties
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Cafe>(entity =>
        //    {
        //        entity.ToTable("Cafe");
        //    });

        //    modelBuilder.Entity<Employee>(entity =>
        //    {
        //        entity.ToTable("Employee");
        //    });

        //}

    }
}
