using Microsoft.AspNetCore.Identity;

namespace GeoLocationAPI.Entities
{
    public class AplicationUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
