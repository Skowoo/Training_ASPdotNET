using AplikacjaLabyData;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaLaby.Controllers
{
    [Route("api/owners")] // Access route to API
    [ApiController] // Markup of API controller
    public class OwnerApiController : ControllerBase // API controller derives directly from ControllerBase
    {
        private readonly AppDbContext _context;

        public OwnerApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string searchKey)
        {
            return Ok(
                _context.Owners
                .Where(x => x.Name!.Contains(searchKey) || x.Surname!.Contains(searchKey))
                .ToList()
                ); // Method return list of items. Ok is body of response with code 200 - OK
        }
    }
}
