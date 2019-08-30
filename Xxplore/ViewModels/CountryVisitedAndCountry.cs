using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xxplore.Models;

namespace Xxplore.ViewModels
{
    public class CountryVisitedAndCountry
    {
      
        public List<Country> CountryList { get; private set; }
        public List<CountryVisited> CountryVisitedList { get; private set; }

        public CountryVisitedAndCountry(List<Country> country, List<CountryVisited> countryVisited) {
           CountryList = country;
           CountryVisitedList = countryVisited;
        }

}
}
