using CompanyPortal.Data;
using CompanyPortal.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Controllers
{
    public class TeamMembersController : Controller
    {
        private readonly CompanyDBContext _context;

        public TeamMembersController(CompanyDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = new EmployeeRoleViewModel
            {
                EmployeeUsers = await _context.TblEmployees.Where(e => e.RoleId == 1).ToListAsync(),
                HrUsers = await _context.TblEmployees.Where(e => e.RoleId == 2).ToListAsync(),
                CampusUsers = await _context.TblEmployees.Where(e => e.RoleId == 3).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(int employeeId, int newRoleId)
        {
            var employee = await _context.TblEmployees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            employee.RoleId = newRoleId;
            _context.Update(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
