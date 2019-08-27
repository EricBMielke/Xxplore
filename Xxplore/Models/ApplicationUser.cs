using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        [NotMapped]
        public bool isSuperAdmin { get; set; }

    }
}
