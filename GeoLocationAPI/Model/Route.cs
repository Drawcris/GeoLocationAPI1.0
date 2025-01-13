using System.Text.Json.Serialization;

namespace GeoLocationAPI.Entities
{
    public class Route
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; } 

        public string DriverId { get; set; } 
        public AplicationUser Driver { get; set; } 

        [JsonIgnore]
        public List<RouteLocation> Locations { get; set; } = new List<RouteLocation>();
        public DateTime Date { get; set; } 
    }

    public class RouteLocation
    {
        public int Id { get; set; }
        public int RouteId { get; set; }

        [JsonIgnore] 
        public Route Route { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
