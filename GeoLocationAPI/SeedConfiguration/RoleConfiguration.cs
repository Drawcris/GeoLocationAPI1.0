using Microsoft.EntityFrameworkCore;
using GeoLocationAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;


namespace GeoLocationAPI.SeedConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
       public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                               new IdentityRole
                               {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                                              new IdentityRole
                                              {
                    Id = "2",
                    Name = "Kierowca",
                    NormalizedName = "KIEROWCA",
                }
                                                         );
        }

    }   
}
