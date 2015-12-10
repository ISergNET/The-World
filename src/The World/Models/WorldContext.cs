using Microsoft.Data.Entity;

namespace The_World.Models
{
    public class WorldContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        public WorldContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = Startup.Configuration["Data:WorldContextConnection"];
            optionsBuilder.UseSqlServer(conn);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
