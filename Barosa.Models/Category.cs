using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBarosa.Models
{
    public class Category
    {
        [Key]
        [DisplayName("Category ID")]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Type of breed")]
        public string? TypeOfBreed { get; set; }
    }

}
