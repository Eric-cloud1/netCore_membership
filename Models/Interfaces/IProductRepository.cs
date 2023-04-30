using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberApi.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Product { get; }
        Task<String> Category(int? productID);
        void SaveProduct(Product product);
        Task<bool> SaveProductAsync(Product product);
        Product DeleteProduct(int productID);
        Task<bool> DeleteProductAsync(int productID);
    }
}
