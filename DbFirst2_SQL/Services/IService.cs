using DbFirst2_SQL.Models;

namespace DbFirst2_SQL.Services
{
    public interface IService
    {
        IQueryable<Product> Products { get; }
    }
}
