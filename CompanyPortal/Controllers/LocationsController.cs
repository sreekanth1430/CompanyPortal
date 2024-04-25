using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyPortal.Data;
using CompanyPortal.Models;

namespace CompanyPortal.Controllers
{
    public class LocationsController : Controller
    {
        private readonly CompanyDBContext _context;

        public LocationsController(CompanyDBContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var companyDBContext = _context.TblLocations.Include(t => t.Company);
            return View(await companyDBContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblLocation = await _context.TblLocations
    .Include(t => t.Company)
    .FirstOrDefaultAsync(m => m.LocationID == id);
            if (tblLocation == null)
            {
                return NotFound();
            }
            return View(tblLocation);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId");
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,Name,Address,PhoneNumber,LocationCode,CompanyId")] TblLocation tblLocation)
        {

            _context.Add(tblLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblLocation = await _context.TblLocations.FindAsync(id);
            if (tblLocation == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId", tblLocation.CompanyId);
            return View(tblLocation);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,Name,Address,PhoneNumber,LocationCode,CompanyId")] TblLocation tblLocation)
        {
            if (id != tblLocation.LocationID)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tblLocation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblLocationExists(tblLocation.LocationID))
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

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblLocation = await _context.TblLocations
    .Include(t => t.Company)
    .FirstOrDefaultAsync(m => m.LocationID == id);
            if (tblLocation == null)
            {
                return NotFound();
            }
            return View(tblLocation);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblLocation = await _context.TblLocations.FindAsync(id);
            if (tblLocation != null)
            {
                _context.TblLocations.Remove(tblLocation);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblLocationExists(int id)
        {
            return _context.TblLocations.Any(e => e.LocationID == id);
        }
    }
}
