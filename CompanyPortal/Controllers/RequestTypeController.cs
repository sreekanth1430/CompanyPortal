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
    public class RequestTypeController : Controller
    {
        private readonly CompanyDBContext _context;

        public RequestTypeController(CompanyDBContext context)
        {
            _context = context;
        }

        // GET: RequestType
        public async Task<IActionResult> Index()
        {
            var companyDBContext = _context.TblRequestType.Include(t => t.Company);
            return View(await companyDBContext.ToListAsync());
        }

        // GET: RequestType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblRequestType = await _context.TblRequestType
                .Include(t => t.Company)
                .FirstOrDefaultAsync(m => m.RequestTypeId == id);
            if (tblRequestType == null)
            {
                return NotFound();
            }
            return View(tblRequestType);
        }

        // GET: RequestType/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId");
            return View();
        }

        // POST: RequestType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestTypeId,RequestTypeName,CompanyId")] TblRequestType tblRequestType)
        {
            _context.Add(tblRequestType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RequestType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblRequestType = await _context.TblRequestType.FindAsync(id);
            if (tblRequestType == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.TblCompanies, "CompanyId", "CompanyId", tblRequestType.CompanyId);
            return View(tblRequestType);
        }

        // POST: RequestType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestTypeId,RequestTypeName,CompanyId")] TblRequestType tblRequestType)
        {
            if (id != tblRequestType.RequestTypeId)
            {
                return NotFound();
            }
            try
            {
                _context.Update(tblRequestType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblRequestTypeExists(tblRequestType.RequestTypeId))
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

        // GET: RequestType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblRequestType = await _context.TblRequestType
                .Include(t => t.Company)
                .FirstOrDefaultAsync(m => m.RequestTypeId == id);
            if (tblRequestType == null)
            {
                return NotFound();
            }
            return View(tblRequestType);
        }

        // POST: RequestType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblRequestType = await _context.TblRequestType.FindAsync(id);
            if (tblRequestType != null)
            {
                var requestTypesForCompany = _context.TblRequestType.Where(rt => rt.CompanyId == tblRequestType.CompanyId);
                if (requestTypesForCompany.Count() > 1)
                {
                    _context.TblRequestType.Remove(tblRequestType);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("", "A company must have at least 1 RequestType record.");
                    return View(tblRequestType);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TblRequestTypeExists(int id)
        {
            return _context.TblRequestType.Any(e => e.RequestTypeId == id);
        }
    }
}
