using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CK_ASP_NET_CORE.Repository.Validation;

namespace CK_ASP_NET_CORE.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }   
        public decimal Price { get; set; }
        public int BrandId { get; set; }
		public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [KiemTra]
        public IFormFile? ImageUpload { get; set; }
	}
}
