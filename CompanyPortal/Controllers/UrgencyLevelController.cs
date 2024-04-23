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
    public class UrgencyLevelController : Controller
    {
        private readonly CompanyDBContext _context;

        public UrgencyLevelController(CompanyDBContext context)
        {
            _context = context;
        }

        // GET: UrgencyLevel
        public async Task<IActionResult> Index()
        {
            var companyDBContext = _context.TblUrgencyLevel.Include(t => t.Company);
            return View(await companyDBContext.ToListAsync());
        }

        // GET: UrgencyLevel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUrgencyLevel = await _context.TblUrgencyLevel
                .Include(t => t.Company)
                .FirstOrDefaultAsync(m => m.UrgencyLevelId == id);
            if (tblUrgencyLevel == null)
            {
                return NotFound();
            }

            return View(tblUrgencyLevel);
        }

        // GET: UrgencyLevel/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId");
            return View();
        }

        // POST: UrgencyLevel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrgencyLevelId,UrgencyLevelName,UrgencyOrder,CompanyId")] TblUrgencyLevel tblUrgencyLevel)
        {

            _context.Add(tblUrgencyLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UrgencyLevel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUrgencyLevel = await _context.TblUrgencyLevel.FindAsync(id);
            if (tblUrgencyLevel == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId", tblUrgencyLevel.CompanyId);
            return View(tblUrgencyLevel);
        }

        // POST: UrgencyLevel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrgencyLevelId,UrgencyLevelName,UrgencyOrder,CompanyId")] TblUrgencyLevel tblUrgencyLevel)
        {
            if (id != tblUrgencyLevel.UrgencyLevelId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(tblUrgencyLevel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUrgencyLevelExists(tblUrgencyLevel.UrgencyLevelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            /*
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId", tblUrgencyLevel.CompanyId);
            return View(tblUrgencyLevel);
            */
        }

        // GET: UrgencyLevel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUrgencyLevel = await _context.TblUrgencyLevel
                .Include(t => t.Company)
                .FirstOrDefaultAsync(m => m.UrgencyLevelId == id);
            if (tblUrgencyLevel == null)
            {
                return NotFound();
            }

            return View(tblUrgencyLevel);
        }

        // POST: UrgencyLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblUrgencyLevel = await _context.TblUrgencyLevel.FindAsync(id);
            if (tblUrgencyLevel != null)
            {
                var requestTypesForCompany = _context.TblUrgencyLevel.Where(rt => rt.CompanyId == tblUrgencyLevel.CompanyId);
                if (requestTypesForCompany.Count() > 1)
                {
                    _context.TblUrgencyLevel.Remove(tblUrgencyLevel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("", "A company must have at least 1 Urgency Level record.");
                    return View(tblUrgencyLevel);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TblUrgencyLevelExists(int id)
        {
            return _context.TblUrgencyLevel.Any(e => e.UrgencyLevelId == id);
        }
    }
}
