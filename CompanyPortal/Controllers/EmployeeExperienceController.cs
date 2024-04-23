using CompanyPortal.Data;
using CompanyPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Controllers
{
    public class EmployeeExperienceController : Controller
    {

        private readonly CompanyDBContext _context;
        public EmployeeExperienceController(CompanyDBContext context)
        {
            _context = context;
        }


        // GET: EmployeeExperience
        public async Task<IActionResult> Index(int? id)
        {
            var contacts = await _context.TblContacts.ToListAsync();
            ViewData["Contacts"] = contacts;

            TblContact contact = null;
            if (id != null)
            {
                contact = await _context.TblContacts.FirstOrDefaultAsync(m => m.Id == id);
                if (contact != null)
                {
                    ViewData["Contact"] = contact;
                }
            }

            return View(contact ?? new TblContact());
        }

        // GET: EmployeeExperience/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var contact = id.HasValue
                ? await _context.TblContacts.FindAsync(id.Value)
                : new CompanyPortal.Models.TblContact();

            return View(contact);
        }


        /*        // GET: EmployeeExperience/Edit/5
                [HttpPost]
                public async Task<IActionResult> Edit(TblContact contact)
                {
                    if (contact.Id == 0)
                    {
                        _context.Add(contact);
                    }
                    else
                    {
                        _context.Update(contact);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }*/


        // POST: EmployeeExperience/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,Name,Title,Email,Phone,IsActive,DisplayEmployee,InsertDate,ContactOrder")] TblContact tblContact)
        {
            if (id != tblContact.Id)
            {
                return NotFound();
            }

            // Get the existing contact from the database
            var existingContact = await _context.TblContacts.FindAsync(id);
            if (existingContact == null)
            {
                return NotFound();
            }

            // Update the properties of the existing contact
            existingContact.Name = tblContact.Name;
            existingContact.Title = tblContact.Title;
            existingContact.Email = tblContact.Email;
            existingContact.Phone = tblContact.Phone;
            existingContact.IsActive = tblContact.IsActive;
            existingContact.DisplayEmployee = tblContact.DisplayEmployee;
            //existingContact.Insert_date = tblContact.Insert_date;
            //existingContact.ContactOrder = tblContact.ContactOrder;

            try
            {
                _context.Update(existingContact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContactExists(tblContact.Id))
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
        // GET: EmployeeExperience/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblContact = await _context.TblContacts.FirstOrDefaultAsync(m => m.Id == id);
            if (tblContact == null)
            {
                return NotFound();
            }

            _context.TblContacts.Remove(tblContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool TblContactExists(int id)
        {
            return _context.TblContacts.Any(e => e.Id == id);
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,Name,Title,Email,Phone,IsActive,DisplayEmployee,Insert_date,ContactOrder")] TblContact tblContact)
        {
            // Map CompanyId to default i.e CompanyId = 5
            tblContact.CompanyId = 5;

            // Map ContactOrder to null
            tblContact.ContactOrder = null;

            // Map Insert_date to DateTime.Now
            tblContact.Insert_date = DateTime.Now;

            _context.Add(tblContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
