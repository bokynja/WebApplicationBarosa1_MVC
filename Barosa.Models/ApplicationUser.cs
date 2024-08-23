using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationBarosa.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Name { get; set; }

        public string? StreedAddress { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}
