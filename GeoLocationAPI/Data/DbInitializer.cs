using GeoLocationAPI.Entities;
namespace GeoLocationAPI.Data
{
    public class DbInitializer
    {
        public static void Init(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Routes.Any())
            {
                return;
            }

            var routes = new Entities.Route[]
            {
                new Entities.Route { Name = "Trasa test", Status = "w toku", DriverId = "1", Date = DateTime.Now },           
            };

            foreach (Entities.Route r in routes)
            {
                context.Routes.Add(r);
            }
         

            context.SaveChanges();
        }
    }
}
