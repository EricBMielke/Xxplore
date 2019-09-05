using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class SelectedCountry
    {
        [Display(Name = "Highlight")]
        public bool showAsSelected { get; set; }
        [Display(Name = "CountryAcronym")]
        public string id { get; set; }
    }
}
