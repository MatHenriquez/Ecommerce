using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(60, ErrorMessage = "Category name must be less than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required")]
        [MaxLength(150, ErrorMessage = "Category description must be less than 150 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category status is required")]
        public bool Status { get; set; }
    }
}
