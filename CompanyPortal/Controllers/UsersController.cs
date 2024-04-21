using CompanyPortal.Data;
using CompanyPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Controllers
{
    public class UsersController : Controller
    {
        private readonly CompanyDBContext _context;

        public UsersController(CompanyDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.TblUsers
                    .Include(u => u.Company)
                    .Include(u => u.UserGroupMapping)
                    .ThenInclude(ug => ug.UserGroup)
                    .ToListAsync();
            return View(users);
        }

        // GET: Users/Assign/5
        public async Task<IActionResult> Assign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await _context.TblUsers
                    .Include(u => u.Company)
                    .Include(u => u.UserGroupMapping)
                    .ThenInclude(ug => ug.UserGroup)
                    .ToListAsync();

            var user = users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["UserGroups"] = new SelectList(_context.TblUserGroups, "GroupId", "GroupName", user.UserGroupMapping.First().GroupId);
            return View(user);
        }

        /* // POST: Users/Assign/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Assign(int id, [Bind("UserId,UserName,CompanyId,UserGroupId")] TblUser user)
         {
             if (id != user.UserId)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(user);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!UserExists(user.UserId))
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
             ViewData["UserGroupId"] = new SelectList(_context.TblUserGroups, "GroupId", "GroupName", user.UserId);
             return View(user);
         }

         private bool UserExists(int id)
         {
             return _context.TblUsers.Any(e => e.UserId == id);
         }*/

        [HttpPost]
        public async Task<IActionResult> Assign(int id, int groupId)
        {
            var user = await _context.TblUsers
        .Include(u => u.UserGroupMapping)
        .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var mapping = user.UserGroupMapping.FirstOrDefault();
            if (mapping != null)
            {
                _context.UserGroupMappings.Remove(mapping);
                await _context.SaveChangesAsync();  // Save changes after removing the old mapping
            }

            var newMapping = new UserGroupMapping { UserId = id, GroupId = groupId };
            _context.UserGroupMappings.Add(newMapping);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
