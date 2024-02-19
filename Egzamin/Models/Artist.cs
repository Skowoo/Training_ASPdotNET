using Egzamin.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egzamin;
public class Artist
{
    public int ArtistId { get; set; }

    public string Name { get; set; }

    ICollection<Album> Albums { get; set;}
}