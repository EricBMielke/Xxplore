using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class CountryVisited
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "CountryId")]
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }
        [NotMapped]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        [Display(Name = "UserId")]
        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        [Display(Name = "Highlight Of Trip")]
        public string HighlightOfTrip { get; set; }
        [Display(Name = "Rating Of Trip")]
        public double RatingOfTrip { get; set; }
        [Display(Name = "Start Of Trip")]
        public DateTime StartOfTrip { get; set; }
        [Display(Name = "End Of Trip")]
        public DateTime EndOfTrip { get; set; }
        [Display(Name = "Photos Of Trip")]
        [NotMapped]
        public string [] PhotosOfTrip { get; set; }
        [NotMapped]
        public bool hasVisited { get; set; }
    }
}
