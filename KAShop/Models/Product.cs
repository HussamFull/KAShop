using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KAShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }





    }
}
