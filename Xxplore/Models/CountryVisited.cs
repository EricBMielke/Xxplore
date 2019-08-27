using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xplore.Models
{
    public class CountryVisited
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "CountryId")]
        [ForeignKey("CountryId")]
        public int? CountryId { get; set; }
        [Display(Name = "Comments")]
        [NotMapped]
        public string [] Comments { get; set; }
        [Display(Name = "HighlightOfTrip")]
        public string HighlightOfTrip { get; set; }
        [Display(Name = "RatingOfTrip")]
        public double RatingOfTrip { get; set; }
        [Display(Name = "StartOfTrip")]
        public DateTime StartOfTrip { get; set; }
        [Display(Name = "EndOfTrip")]
        public DateTime EndOfTrip { get; set; }
        [Display(Name = "PhotosOfTrip")]
        [NotMapped]
        public string [] PhotosOfTrip { get; set; }
    }
}
