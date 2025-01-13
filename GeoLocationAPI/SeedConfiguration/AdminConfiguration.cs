using GeoLocationAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoLocationAPI.SeedConfiguration
{
    public class AdminConfiguration : IEntityTypeConfiguration<AplicationUser>
    {
        public void Configure(EntityTypeBuilder<AplicationUser> builder)
        {
            var hasher = new PasswordHasher<AplicationUser>();

            var adminUser = new AplicationUser
            {
                Id = "1",
                UserName = "Admin@admin.pl",
                FullName = "Admin",
                NormalizedUserName = "ADMIN@ADMIN.PL",
                Email = "Admin@admin.pl",
                NormalizedEmail = "ADMIN@ADMIN.PL",
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            builder.HasData(adminUser);
        }
    }
}