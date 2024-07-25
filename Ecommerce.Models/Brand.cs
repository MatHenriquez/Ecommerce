using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Brand name is required")]
        [MaxLength(60, ErrorMessage = "Brand name must be less than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Brand description is required")]
        [MaxLength(150, ErrorMessage = "Brand description must be less than 150 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Brand status is required")]
        public bool Status { get; set; }
    }
}
