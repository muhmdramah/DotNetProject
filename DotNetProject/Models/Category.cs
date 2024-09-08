using System.ComponentModel.DataAnnotations;

namespace DotNetProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
