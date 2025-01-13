using GeoLocationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using GeoLocationAPI.SeedConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GeoLocationAPI.Data
{
    public class DataContext : IdentityDbContext<AplicationUser, IdentityRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Entities.Route> Routes { get; set; }
        public DbSet<RouteLocation> RouteLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new AdminConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            builder.Entity<RouteLocation>()
                .HasKey(rl => rl.Id); 

            builder.Entity<RouteLocation>()
                .HasOne(rl => rl.Route)
                .WithMany(r => r.Locations)
                .HasForeignKey(rl => rl.RouteId);
        }
    }

} 