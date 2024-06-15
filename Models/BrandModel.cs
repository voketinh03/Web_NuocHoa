using System.ComponentModel.DataAnnotations;

namespace CK_ASP_NET_CORE.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set;}
        public int Status { get; set; }
    }
}
