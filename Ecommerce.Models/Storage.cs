using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters")]
        [MinLength(6, ErrorMessage = "Name can't be less than 3 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(150, ErrorMessage = "Description can't be more than 150 characters")]
        [MinLength(15, ErrorMessage = "Description can't be less than 15 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }
    }
}
