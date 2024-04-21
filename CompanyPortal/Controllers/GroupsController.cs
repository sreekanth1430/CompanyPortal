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
    public class GroupsController : Controller
    {
        private readonly CompanyDBContext _context;

        public GroupsController(CompanyDBContext context)
        {
            _context = context;
        }

        // GET: UserGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblUserGroups.ToListAsync());
        }

        // GET: UserGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUserGroup = await _context.TblUserGroups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (tblUserGroup == null)
            {
                return NotFound();
            }

            return View(tblUserGroup);
        }

        // GET: UserGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,GroupName")] TblUserGroup tblUserGroup)
        {

            _context.Add(tblUserGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: UserGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUserGroup = await _context.TblUserGroups.FindAsync(id);
            if (tblUserGroup == null)
            {
                return NotFound();
            }
            return View(tblUserGroup);
        }

        // POST: UserGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,GroupName")] TblUserGroup tblUserGroup)
        {
            if (id != tblUserGroup.GroupId)
            {
                return NotFound();
            }


            try
            {
                _context.Update(tblUserGroup);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserGroupExists(tblUserGroup.GroupId))
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

        // GET: UserGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUserGroup = await _context.TblUserGroups
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (tblUserGroup == null)
            {
                return NotFound();
            }

            return View(tblUserGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblUserGroup = await _context.TblUserGroups.FindAsync(id);
            if (tblUserGroup != null)
            {
                _context.TblUserGroups.Remove(tblUserGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblUserGroupExists(int id)
        {
            return _context.TblUserGroups.Any(e => e.GroupId == id);
        }
    }
}
