using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBarosa.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SKU { get; set; }

        [Required]
        [Display(Name = "List price")]
        [Range(1, double.MaxValue, ErrorMessage = "List price must be greater than zero.")]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public Sex Gender { get; set; }
        [Required]

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }
}
