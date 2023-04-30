using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberApi.Data;
using Microsoft.EntityFrameworkCore;
namespace MemberApi.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Product =>  context.Product;

       // public async Task<IQueryable<Product>> product => context.Set<Product>().ToListAsync();

        public async Task<String> Category(int? productID)
        {
            if(productID ==null)
                return string.Empty;

            Product dbEntry = await context.Product
              .SingleOrDefaultAsync(p => p.ProductId == productID);
            if (dbEntry != null)
                return dbEntry.Category;

            return string.Empty;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Product.Add(product);
            }
            else
            {
                Product dbEntry = context.Product
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
             context.SaveChanges();
        }

        public async Task<bool> SaveProductAsync(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Product.Add(product);
            }
            else
            {
                Product dbEntry = context.Product
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
             int x = await context.SaveChangesAsync();

            if(x == 0)
                return false;

            return true;
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Product
                .FirstOrDefault(p => p.ProductId == productID);
            if (dbEntry != null)
            {
                context.Product.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public async Task<bool> DeleteProductAsync(int productID)
        {
            Product dbEntry = await context.Product
                .SingleOrDefaultAsync(p => p.ProductId == productID);

            if (dbEntry != null)
                context.Product.Remove(dbEntry);

          int x = await  context.SaveChangesAsync();

            if (x == 0)
                return false;

            return  true;
        }
    }
}
