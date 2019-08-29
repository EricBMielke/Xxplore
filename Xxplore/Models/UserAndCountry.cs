using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class UserAndCountry
    {
        [Key]
        public int UserAndCountryId { get; set; }
        public int CountryVisitedId { get; set; }
        public int UserProfileId { get; set; }
    }
}
