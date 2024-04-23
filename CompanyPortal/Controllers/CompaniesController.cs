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
    public class CompaniesController : Controller
    {
        private readonly CompanyDBContext _context;

        public CompaniesController(CompanyDBContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblCompanies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompany = await _context.TblCompanies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (tblCompany == null)
            {
                return NotFound();
            }

            return View(tblCompany);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName")] TblCompany tblCompany)
        {

            // Create a new TblCompany
            _context.Add(tblCompany);

            // Create a new TblRequestType
            var requestType = new TblRequestType
            {
                RequestTypeName = "General",
                CompanyId = tblCompany.CompanyId,
                Company = tblCompany
            };
            _context.Add(requestType);

            // Create a new TblUserGroup
            var userGroup = new TblUserGroup
            {
                GroupName = "General",
                CompanyId = tblCompany.CompanyId,
                Company = tblCompany
            };
            _context.Add(userGroup);

            // Get the maximum UrgencyOrder and increment it by 1
            int urgencyMaxOrder = _context.TblUrgencyLevel.Max(c => c.UrgencyOrder);

            // Create a new TblUrgencyLevel
            var urgencyLevel = new TblUrgencyLevel
            {
                UrgencyLevelName = "General",
                CompanyId = tblCompany.CompanyId,
                Company = tblCompany,
                UrgencyOrder = urgencyMaxOrder + 1
            };

            _context.Add(urgencyLevel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompany = await _context.TblCompanies.FindAsync(id);
            if (tblCompany == null)
            {
                return NotFound();
            }
            return View(tblCompany);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName")] TblCompany tblCompany)
        {
            if (id != tblCompany.CompanyId)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tblCompany);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCompanyExists(tblCompany.CompanyId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            return View(tblCompany);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCompany = await _context.TblCompanies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (tblCompany == null)
            {
                return NotFound();
            }

            return View(tblCompany);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCompany = await _context.TblCompanies.FindAsync(id);
            if (tblCompany != null)
            {
                _context.TblCompanies.Remove(tblCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCompanyExists(int id)
        {
            return _context.TblCompanies.Any(e => e.CompanyId == id);
        }
    }
}
