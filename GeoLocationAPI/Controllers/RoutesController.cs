using GeoLocationAPI.Data;
using GeoLocationAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly DataContext _context;

    public RoutesController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateRoute([FromBody] RouteDto routeDto)
    {
        var route = new GeoLocationAPI.Entities.Route
        {
            DriverId = routeDto.DriverId,
            Name = routeDto.Name,
            Status = routeDto.Status = "W toku",
            Date = DateTime.Now,
            Locations = routeDto.Locations.Select(l => new RouteLocation
            {
                Address = l.Address,
                City = l.City,
                ZipCode = l.ZipCode
            }).ToList()
        };

        _context.Routes.Add(route);
        _context.SaveChanges();

        return Ok(new RouteDto
        {
            Id = route.Id,
            DriverId = route.DriverId,
            Name = route.Name,
            Locations = route.Locations.Select(l => new LocationDto
            {
                Address = l.Address,
                City = l.City,
                ZipCode = l.ZipCode
            }).ToList()
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetByRouteId(int id)
    {
        var route = _context.Routes
            .Include(r => r.Locations)
            .FirstOrDefault(r => r.Id == id);

        if (route == null)
            return NotFound();

        var routeDto = new RouteDto
        {
            Id = route.Id,
            DriverId = route.DriverId,
            Name = route.Name,
            Locations = route.Locations.Select(loc => new LocationDto
            {
                Address = loc.Address,
                City = loc.City,
                ZipCode = loc.ZipCode
            }).ToList()
        };

        return Ok(routeDto);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetRoutesByUserId(string userId)
    {
        var routes = await _context.Routes.Where(r => r.DriverId == userId).ToListAsync();
        return Ok(routes);
    }

    [HttpGet]
    public IActionResult GetAllRoutes()
    {
        var routes = _context.Routes
            .Include(r => r.Locations)
            .Include(r => r.Driver) // Dodano włączenie użytkownika
            .ToList();

        var routeDtos = routes.Select(route => new RouteDto
        {
            Id = route.Id,
            DriverId = route.DriverId,
            DriverEmail = route.Driver.Email, 
            Name = route.Name,
            Status = route.Status,
            Date = route.Date, 
            Locations = route.Locations.Select(loc => new LocationDto
            {
                Address = loc.Address,
                City = loc.City,
                ZipCode = loc.ZipCode
            }).ToList()
        }).ToList();

        return Ok(routeDtos);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoute(int id)
    {
        var route = _context.Routes.FirstOrDefault(r => r.Id == id);
        if (route == null)
            return NotFound();
        _context.Routes.Remove(route);
        _context.SaveChanges();
        return Ok();
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetRoutesByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return NotFound();

        var routes = await _context.Routes
            .Include(r => r.Locations)
            .Where(r => r.DriverId == user.Id)
            .ToListAsync();

        var routeDtos = routes.Select(route => new RouteDto
        {
            Id = route.Id,
            DriverId = route.DriverId,
            Name = route.Name,
            Status = route.Status,
            Locations = route.Locations.Select(loc => new LocationDto
            {
                Address = loc.Address,
                City = loc.City,
                ZipCode = loc.ZipCode
            }).ToList()
        }).ToList();

        return Ok(routeDtos);
    }

    [HttpPut("{id}/complete")]
    public IActionResult CompleteRoute(int id)
    {
        var route = _context.Routes.FirstOrDefault(r => r.Id == id);
        if (route == null)
            return NotFound();
        route.Status = "Ukończony";
        _context.SaveChanges();
        return Ok();
    }

    public class RouteDto
    {
        public int Id { get; set; }
        public string DriverId { get; set; }
        public string DriverEmail { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; } // Dodano pole daty
        public List<LocationDto> Locations { get; set; }
    }

    public class LocationDto
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}