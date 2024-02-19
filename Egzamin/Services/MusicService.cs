using Egzamin.Models;

namespace Egzamin.Services;

public class MusicService
{
    private static AppDbContext _context;

    public MusicService(AppDbContext ct)
    {
        _context = ct;
    }

    public List<Artist> GetAllArtists() => _context.Artists.ToList();

    public List<Album> GetAlbumsByArtistId(int id) => _context.Albums.Where(x => x.ArtistId == id).ToList();


}