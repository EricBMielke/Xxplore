using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xxplore.Data;
using Xxplore.Models;
using Xxplore.ViewModels;

namespace Xxplore.Controllers
{
    public class CountryVisitedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryVisitedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Scoreboard()
        {
            List<double> ratedTrips = new List<double>();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            var countriesFound = _context.CountriesVisited.Where(p => p.UserId == userFound.Id).ToList();
            foreach (CountryVisited item in countriesFound)
            {

                ratedTrips.Add(item.RatingOfTrip);
            }
            //List<int> lst = ints.OfType<int>().ToList();tuff
            double[] ratedTripsArray = ratedTrips.ToArray();
            double[] result = new double[3];
            for (int i = 0; i < ratedTrips.Count; i++)
            {
                if (ratedTrips[i] <= result[0])
                {
                    continue;
                }
                else
                {
                    if (ratedTrips[i] > result[2])
                    {
                        for (int l = 0; l < 2; l++)
                        {
                            result[l] = result[l + 1];
                        }
                        result[2] = ratedTripsArray[i];
                    }
                    else
                    {
                        int indexLeft = 0;
                        int indexRight = 2;
                        int currIndex = 0;
                        while (indexRight - indexLeft > 1)
                        {
                            currIndex = (indexRight + indexLeft) / 2;
                            if (ratedTripsArray[i] >= result[currIndex])
                            {
                                indexLeft = currIndex;
                            }
                            else
                            {
                                indexRight = currIndex;
                            }

                        }

                        for (int l = 0; l < currIndex; l++)
                        {
                            result[l] = result[l + 1];
                        }
                        result[currIndex] = ratedTripsArray[i];
                    }
                }
            }
            List<CountryVisited> topVisitedCountries = new List<CountryVisited>();
            for (int i = 0; i < result.Length; i++)
            {
                topVisitedCountries = _context.CountriesVisited.Where(c => c.RatingOfTrip == result[i]).Select(c => c).ToList();

            }
                return View(topVisitedCountries);
        }
        public async Task<IActionResult> WishList()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            return View(userFound);
        }
        public async Task<IActionResult> Chat()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            return View("Chat");
        }

        public async Task<IActionResult> DreamsToChase()
        {
            //find user
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            //find countries visited of user
            var userCountryVisits = _context.CountriesVisited.Where(p => p.UserId == userFound.Id).Select(cv => cv.CountryId).ToArray();
            var notVisitedCountries = _context.Countries.Where(c => !userCountryVisits.Contains(c.Id));
            return View(notVisitedCountries.ToList());
        }


        // GET: CountryVisiteds
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            var countriesVisited = await _context.CountriesVisited.Where(c => c.UserId == userFound.Id).ToListAsync();
            //var countriesVisited = await _context.CountriesVisited.ToListAsync();
            var dreamList = GetDreamsToChase();
            CountryVisitedAndCountry newView = new CountryVisitedAndCountry(dreamList, countriesVisited);
            return View(newView);
        }
        public async Task<IActionResult> GetHighlights(int? id)
        {
            var countryFound = await _context.CountriesVisited.Where(c => c.CountryId == id).ToListAsync();
            return View(countryFound);
        }

        // GET: CountryVisiteds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryVisited = await _context.CountriesVisited
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryVisited == null)
            {
                return NotFound();
            }

            return View(countryVisited);
        }

        public List<Country> GetDreamsToChase()
        {
            //find user
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            //find countries visited of user
            var userCountryVisits = _context.CountriesVisited.Where(p => p.UserId == userFound.Id).Select(cv => cv.CountryId).ToArray();
            var notVisitedCountries = _context.Countries.Where(c => !userCountryVisits.Contains(c.Id));
            return notVisitedCountries.ToList();
        }

        // GET: CountryVisiteds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountryVisiteds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comments,CountryName,CountryId,UserId,HighlightOfTrip,RatingOfTrip,StartOfTrip,EndOfTrip,hasVisited")] CountryVisited countryVisited)
        {
            if (ModelState.IsValid)
            {
                var userCurrentUser = User.Identity.Name;
                UserProfile selectedUser;
                selectedUser = _context.UserProfile.Where(p => p.Email == User.Identity.Name).Single();
                var countryFound = await _context.Countries
                .FirstOrDefaultAsync(m => m.Name == countryVisited.CountryName);
                countryVisited.CountryId = countryFound.Id;
                countryVisited.UserId = selectedUser.Id;
                countryVisited.StarsOfTrip = ratingToStars(countryVisited.RatingOfTrip);
                _context.Add(countryVisited);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryVisited);
        }

        public string ratingToStars(double rating)

        {
            string ratingImage = "";
            if (rating == 0)
            {
                ratingImage = "";
            }
            else if (rating == 1)
            {
                ratingImage = "&#x2606;";
            }
            else if (rating == 2)
            {
                ratingImage = "&#x2606; &#x2606;";
            }
            else if (rating == 3)
            {
                ratingImage = "&#x2606; &#x2606; &#x2606;"; 
            }
            else if (rating == 4)
            {
                ratingImage = "&#x2606; &#x2606; &#x2606; &#x2606;";
            }
            else if (rating == 5)
            {
                ratingImage = "&#x2606; &#x2606; &#x2606; &#x2606; &#x2606;";
            }
            return ratingImage;
        }

        // GET: CountryVisiteds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryVisited = await _context.CountriesVisited.FindAsync(id);
            if (countryVisited == null)
            {
                return NotFound();
            }
            return View(countryVisited);
        }

        // POST: CountryVisiteds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CountryId,UserId,HighlightOfTrip,RatingOfTrip,StartOfTrip,EndOfTrip,hasVisited,hasntVisited")] CountryVisited countryVisited)
        {
            if (id != countryVisited.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryVisited);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryVisitedExists(countryVisited.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(countryVisited);
        }

        // GET: CountryVisiteds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryVisited = await _context.CountriesVisited
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryVisited == null)
            {
                return NotFound();
            }

            return View(countryVisited);
        }

        // POST: CountryVisiteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countryVisited = await _context.CountriesVisited.FindAsync(id);
            _context.CountriesVisited.Remove(countryVisited);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryVisitedExists(int id)
        {
            return _context.CountriesVisited.Any(e => e.Id == id);
        }
        public string ConvertToCountryName(int id)
        {
            Country selectedCountry = _context.Countries.Where(p => p.Id == id).Single();
            string countryName = selectedCountry.Name;
            return countryName;
        }

        public UserProfile ConnectToUser(Country country)
        {
            var userCurrentUser = User.Identity.Name;
            UserProfile currentUser;
            UserProfile foundUser = null;
            currentUser = _context.UserProfile.Where(p => p.Email == User.Identity.Name).Single();
            var allUsers = _context.UserProfile;
            foreach (UserProfile item in allUsers)
            {
                if(currentUser.NativeLanguage == item.NativeLanguage && country.Name == item.HomeCountry)
                {
                    var countriesFound = _context.CountriesVisited.Where(p => p.Id == country.Id).ToList();
                    foreach (CountryVisited c in countriesFound){
                        if (currentUser.Id == c.UserId) {
                            var countryId = c.Id;
                            foreach (CountryVisited cv in countriesFound)
                            {
                                if(item.Id == cv.UserId && countryId == cv.Id)
                                {
                                    return foundUser;
                                }
                            }
                            foundUser = item;
                            return foundUser;
                        }
                        else
                        {
                            foundUser = item;
                            return foundUser;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
