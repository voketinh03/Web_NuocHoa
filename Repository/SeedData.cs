using Microsoft.EntityFrameworkCore;
using CK_ASP_NET_CORE.Models;

namespace CK_ASP_NET_CORE.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {

                CategoryModel apple = new CategoryModel { Name = "Apple", Slug = "apple", Description = "Trumf the gioi", status = 1 };
                CategoryModel Samsung = new CategoryModel { Name = "Samsung", Slug = "Samsung", Description = "Samsung not trum the world", status = 1 };

                BrandModel Dell = new BrandModel { Name = "Apple", Slug = "apple", Description = "Trumf the gioi", Status = 1 };
                BrandModel Sony = new BrandModel { Name = "Samsung", Slug = "Samsung", Description = "Samsung not trum the world", Status = 1 };

                _context.Products.AddRange(

                    new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Maook the gioi", Image = "1.jpg", Category = apple, Brand = Sony, Price = 1233 },
                    new ProductModel { Name = "Macbook", Slug = "macbook", Description = "Maook the gioi", Image = "1.jpg", Category = apple, Brand = Sony, Price = 1233 }
                );
                _context.SaveChanges();
            }
        }
    }
}
