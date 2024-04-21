using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyPortal.Data;
using CompanyPortal.Models;
using System.Text.RegularExpressions;

namespace CompanyPortal.Controllers
{
    public class UserGroupManagementController : Controller
    {
        private readonly CompanyDBContext _context;
        public UserGroupManagementController(CompanyDBContext context)
        {
            _context = context;
        }
        /* public async Task<IActionResult> Index()
         {
             var viewModel = new Models.ViewModels.UserGroupViewModel
             {
                 UserGroups = await _context.TblUserGroups.ToListAsync(),
                 GroupUsers = new List<TblUser>(),
                 OtherUsers = new List<TblUser>()
             };

             return View(viewModel);
         }*/

        public async Task<IActionResult> Index(int? groupId)
        {
            var viewModel = new Models.ViewModels.UserGroupViewModel
            {
                UserGroups = await _context.TblUserGroups.ToListAsync(),
                GroupUsers = groupId.HasValue ?
                    await _context.UserGroupMappings.Where(mapping => mapping.GroupId == groupId)
                        .Select(mapping => mapping.User)
                        .ToListAsync() :
                    new List<TblUser>(),
                OtherUsers = groupId.HasValue ?
                    await _context.TblUsers.Where(user => !_context.UserGroupMappings.Any(mapping => mapping.UserId == user.UserId && mapping.GroupId == groupId))
                        .ToListAsync() :
                    new List<TblUser>(),
                    SelectedGroupId = groupId == null ? 0 : groupId.Value
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Remove(int userId)
        {
            int groupId = 0;
            var mapping = await _context.UserGroupMappings.FirstOrDefaultAsync(m => m.UserId == userId);
            if (mapping != null)
            {
                groupId = mapping.GroupId; // Store the groupId before removing the mapping

                _context.UserGroupMappings.Remove(mapping);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { groupId = groupId });
        }

        public async Task<IActionResult> Add(int userId, int groupId)
        {
            var mapping = new UserGroupMapping { UserId = userId, GroupId = groupId };
            _context.UserGroupMappings.Add(mapping);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { groupId = groupId });
        }
    }
}
