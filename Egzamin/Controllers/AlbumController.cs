using Egzamin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Egzamin.Controllers
{
    public class AlbumController : Controller
    {
        static MusicService _ms;

        public AlbumController(MusicService ms)
        {
            _ms = ms;
        }

        [HttpGet]
        public IActionResult Index(int? ArtistId)
        {
            ViewBag.Artists = _ms.GetAllArtists();

            if (ArtistId is not null)
            {
                if (!_ms.GetAllArtists().Any(x => x.ArtistId == (int)ArtistId))
                    return NotFound();

                var target = _ms.GetAlbumsByArtistId((int)ArtistId);
                return View(target);
            }

            return View();
        }
    }
}
