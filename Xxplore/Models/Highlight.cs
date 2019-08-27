using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class Highlight
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "CountryId")]
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }
        [Display(Name = "Highlight")]
        public string UserHighlight { get; set; }
    }
}
