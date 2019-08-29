using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xxplore.Models
{
    public class CountryVisitedUsers
    {
        [Key]
        public int CountryVisitedUsersId { get; set; }
        public int CountryVisitedId { get; set; }

        public int UserProfileId { get; set; }
    }
}