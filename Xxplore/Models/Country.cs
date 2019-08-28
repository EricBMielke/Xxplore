using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Country Code")]
        public string Code { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
