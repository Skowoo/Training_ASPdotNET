using DbFirst1.Models;

namespace DbFirst1.Services
{
    public interface IProductService
    {
        IQueryable<Product> Products { get; }
    }
}
