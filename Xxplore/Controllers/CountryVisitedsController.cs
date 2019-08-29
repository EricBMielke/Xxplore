using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xxplore.Data;
using Xxplore.Models;

namespace Xxplore.Controllers
{
    public class CountryVisitedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryVisitedsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> WishList(int? id)
        {
            var userFound = await _context.UserProfile.FirstOrDefaultAsync(m => m.Id == id);
            return View();
        }

        public async Task<IActionResult> VisitedAndHasntVisited(int? id)
        {
            var userFound = await _context.UserProfile.FirstOrDefaultAsync(m => m.Id == id);
            return View();
        }

        // GET: CountryVisiteds
        public async Task<IActionResult> Index()
        {
            return View(await _context.CountriesVisited.ToListAsync());
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
    }
}
