﻿using System;
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
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            //Data to get user trips back and then parse which are the top 3
            return View(userFound);
        }
        public async Task<IActionResult> WishList()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.Users.Where(p => p.Id == user).Single();
            var userFound = _context.UserProfile.Where(p => p.Email == userName.UserName).Single();
            return View(userFound);
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
        public async Task<IActionResult> Create([Bind("Id,CountryName,CountryId,UserId,HighlightOfTrip,RatingOfTrip,StartOfTrip,EndOfTrip,hasVisited")] CountryVisited countryVisited)
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
                _context.Add(countryVisited);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryVisited);
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
    }
}
