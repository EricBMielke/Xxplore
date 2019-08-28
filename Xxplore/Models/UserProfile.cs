﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name = "Home Country")]
        public string HomeCountry { get; set; }
        [Display(Name = "Country 1 to Visit")]
        public int WishList1 { get; set; }
        [Display(Name = "Country 2 to Visit")]
        public int WishList2 { get; set; }
        [Display(Name = "Country 3 to Visit")]
        public int WishList3 { get; set; }
        [Display(Name = "Countries Visited")]
        [NotMapped]
        public string [] CountriesVisited { get; set; }
    }
}
