using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "HomeCountry")]
        public string HomeCountry { get; set; }
        [Display(Name = "CountriesVisited")]
        [NotMapped]
        public string [] CountriesVisited { get; set; }
    }
}
