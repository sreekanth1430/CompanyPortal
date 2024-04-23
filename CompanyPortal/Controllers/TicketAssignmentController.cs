using CompanyPortal.Data;
using CompanyPortal.Models;
using CompanyPortal.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Controllers
{
    public class TicketAssignmentController : Controller
    {
        private readonly CompanyDBContext _context;

        public TicketAssignmentController(CompanyDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var viewModel = new TicketAssignmentViewModel() { };
            {
                viewModel.Groups = new SelectList(_context.TblUserGroups, "GroupId", "GroupName");
                viewModel.UrgencyLevels = new SelectList(_context.TblUrgencyLevel, "UrgencyLevelId", "UrgencyLevelName");
                viewModel.RequestTypes = new SelectList(_context.TblRequestType, "RequestTypeId", "RequestTypeName");
                viewModel.Users = new SelectList(_context.TblUsers, "UserId", "UserName");
                viewModel.TicketAssignments = _context.TblTicketAssignment.ToList();
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(TicketAssignmentViewModel viewModel)
        {
            TblTicketAssignment ticketAssignment = new()
            {
                GroupId = viewModel.SelectedGroupId,
                UrgencyLevelId = viewModel.SelectedUrgencyLevelId,
                RequestTypeId = viewModel.SelectedRequestTypeId,
                UserId = viewModel.SelectedUserId,

                GroupName = _context.TblUserGroups.FirstOrDefault(g => g.GroupId == viewModel.SelectedGroupId)?.GroupName,
                UrgencyLevelName = _context.TblUrgencyLevel.FirstOrDefault(u => u.UrgencyLevelId == viewModel.SelectedUrgencyLevelId)?.UrgencyLevelName,
                RequestTypeName = _context.TblRequestType.FirstOrDefault(r => r.RequestTypeId == viewModel.SelectedRequestTypeId)?.RequestTypeName,
                UserName = _context.TblUsers.FirstOrDefault(u => u.UserId == viewModel.SelectedUserId)?.UserName
            };

            if (_context.TblTicketAssignment.Any(t => t.GroupId == ticketAssignment.GroupId && t.UrgencyLevelId == ticketAssignment.UrgencyLevelId && t.RequestTypeId == ticketAssignment.RequestTypeId))
            {
                ModelState.AddModelError("", "The below Ticket Assignment combination already exists for a user. Please select different Combinations");

                viewModel.Groups = new SelectList(_context.TblUserGroups, "GroupId", "GroupName");
                viewModel.UrgencyLevels = new SelectList(_context.TblUrgencyLevel, "UrgencyLevelId", "UrgencyLevelName");
                viewModel.RequestTypes = new SelectList(_context.TblRequestType, "RequestTypeId", "RequestTypeName");
                viewModel.Users = new SelectList(_context.TblUsers, "UserId", "UserName");
                viewModel.TicketAssignments = _context.TblTicketAssignment.ToList();

                return View("Index", viewModel);
            }

            _context.TblTicketAssignment.Add(ticketAssignment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var ticketAssignment = _context.TblTicketAssignment.Find(id);
            if (ticketAssignment == null)
            {
                return NotFound();
            }

            var viewModel = new TicketAssignmentViewModel
            {
                SelectedGroupId = ticketAssignment.GroupId,
                SelectedUrgencyLevelId = ticketAssignment.UrgencyLevelId,
                SelectedRequestTypeId = ticketAssignment.RequestTypeId,
                SelectedUserId = ticketAssignment.UserId,
                Groups = new SelectList(_context.TblUserGroups, "GroupId", "GroupName"),
                UrgencyLevels = new SelectList(_context.TblUrgencyLevel, "UrgencyLevelId", "UrgencyLevelName"),
                RequestTypes = new SelectList(_context.TblRequestType, "RequestTypeId", "RequestTypeName"),
                Users = new SelectList(_context.TblUsers, "UserId", "UserName"),

                TicketAssignments = _context.TblTicketAssignment.ToList()
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, TicketAssignmentViewModel viewModel)
        {
            var ticketAssignment = _context.TblTicketAssignment.Find(id);
            if (ticketAssignment == null)
            {
                return NotFound();
            }

            ticketAssignment.GroupId = viewModel.SelectedGroupId;
            ticketAssignment.UrgencyLevelId = viewModel.SelectedUrgencyLevelId;
            ticketAssignment.RequestTypeId = viewModel.SelectedRequestTypeId;
            ticketAssignment.UserId = viewModel.SelectedUserId;

            ticketAssignment.GroupName = _context.TblUserGroups.FirstOrDefault(g => g.GroupId == viewModel.SelectedGroupId)?.GroupName;
            ticketAssignment.UrgencyLevelName = _context.TblUrgencyLevel.FirstOrDefault(u => u.UrgencyLevelId == viewModel.SelectedUrgencyLevelId)?.UrgencyLevelName;
            ticketAssignment.RequestTypeName = _context.TblRequestType.FirstOrDefault(r => r.RequestTypeId == viewModel.SelectedRequestTypeId)?.RequestTypeName;
            ticketAssignment.UserName = _context.TblUsers.FirstOrDefault(u => u.UserId == viewModel.SelectedUserId)?.UserName;

            _context.TblTicketAssignment.Update(ticketAssignment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var ticketAssignment = _context.TblTicketAssignment.Find(id);
            if (ticketAssignment == null)
            {
                return NotFound();
            }

            _context.TblTicketAssignment.Remove(ticketAssignment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketAssignment = await _context.TblTicketAssignment
                .FirstOrDefaultAsync(m => m.TicketAssignmentId == id);
            if (ticketAssignment == null)
            {
                return NotFound();
            }

            return View(ticketAssignment);
        }
    }
}
