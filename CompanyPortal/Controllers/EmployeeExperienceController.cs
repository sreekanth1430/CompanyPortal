using CompanyPortal.Data;
using CompanyPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CompanyPortal.Controllers
{
    public class EmployeeExperienceController : Controller
    {

        private readonly CompanyDBContext _context;
        public EmployeeExperienceController(CompanyDBContext context)
        {
            _context = context;
        }

        #region Contact

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


        #endregion Contact

        #region Link

        // GET: Links
        public async Task<IActionResult> LinkIndex(int? id)
        {
            var links = await _context.TblLinks.ToListAsync();
            ViewData["Links"] = links;

            TblLink link = null;
            if (id != null)
            {
                link = await _context.TblLinks.FirstOrDefaultAsync(m => m.Id == id);
                if (link != null)
                {
                    ViewData["Link"] = link;
                }
            }
            return View("LinkIndex", link ?? new TblLink());
        }

        // GET: Links/Edit/5
        public async Task<IActionResult> LinkEdit(int? id)
        {
            var link = id.HasValue
                ? await _context.TblLinks.FindAsync(id.Value)
                : new TblLink();

            return View("LinkIndex", link);
        }

        // POST: Links/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkEdit(int id, [Bind("Id,CompanyId,Title,Url,InsertDate,LinkOrder")] TblLink tblLink)
        {
            if (id != tblLink.Id)
            {
                return NotFound();
            }
            var existingLink = await _context.TblLinks.FindAsync(id);
            if (existingLink == null)
            {
                return NotFound();
            }
            existingLink.Title = tblLink.Title;
            existingLink.Url = tblLink.Url;
            try
            {
                _context.Update(existingLink);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblLinkExists(tblLink.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(LinkIndex));
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> LinkDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblLink = await _context.TblLinks.FirstOrDefaultAsync(m => m.Id == id);
            if (tblLink == null)
            {
                return NotFound();
            }
            _context.TblLinks.Remove(tblLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(LinkIndex));
        }

        private bool TblLinkExists(int id)
        {
            return _context.TblLinks.Any(e => e.Id == id);
        }

        // POST: Links/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkCreate([Bind("Id,CompanyId,Title,Url,InsertDate,LinkOrder")] TblLink tblLink)
        {
            // Map CompanyId to default i.e CompanyId = 5
            tblLink.CompanyId = 5;

            tblLink.LinkOrder = null;
            tblLink.Insert_date = DateTime.Now;

            _context.Add(tblLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(LinkIndex));
        }

        #endregion Link

        #region Document

        // GET: Documents
        public async Task<IActionResult> DocumentIndex(int? id)
        {
            var documents = await _context.TblCompilenceDocuments.ToListAsync();
            ViewData["Documents"] = documents;

            TblCompilenceDocument document = null;
            if (id != null)
            {
                document = await _context.TblCompilenceDocuments.FirstOrDefaultAsync(m => m.Id == id);
                if (document != null)
                {
                    ViewData["Document"] = document;
                }
            }
            return View("DocumentIndex", document ?? new TblCompilenceDocument());
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> DocumentEdit(int? id)
        {
            var document = id.HasValue
                ? await _context.TblCompilenceDocuments.FindAsync(id.Value)
                : new TblCompilenceDocument();

            return View("DocumentIndex", document);
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocumentEdit(int id, [Bind("Id,CompanyId,Title,DocumentAlias,DocumentAliasLink,Insert_date,DocumentOrder")] TblCompilenceDocument tblCompilenceDocument)
        {
            if (id != tblCompilenceDocument.Id)
            {
                return NotFound();
            }
            var existingDocument = await _context.TblCompilenceDocuments.FindAsync(id);
            if (existingDocument == null)
            {
                return NotFound();
            }
            existingDocument.Title = tblCompilenceDocument.Title;
            existingDocument.DocumentAlias = tblCompilenceDocument.DocumentAlias;
            existingDocument.DocumentAliasLink = tblCompilenceDocument.DocumentAliasLink;
            try
            {
                _context.Update(existingDocument);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCompilenceDocumentExists(tblCompilenceDocument.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(DocumentIndex));
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> DocumentDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblCompilenceDocument = await _context.TblCompilenceDocuments.FirstOrDefaultAsync(m => m.Id == id);
            if (tblCompilenceDocument == null)
            {
                return NotFound();
            }
            _context.TblCompilenceDocuments.Remove(tblCompilenceDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DocumentIndex));
        }

        private bool TblCompilenceDocumentExists(int id)
        {
            return _context.TblCompilenceDocuments.Any(e => e.Id == id);
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocumentCreate([Bind("Id,CompanyId,Title,DocumentAlias,DocumentAliasLink,Insert_date,DocumentOrder")] TblCompilenceDocument tblCompilenceDocument)
        {
            // Map CompanyId to default i.e CompanyId = 5
            tblCompilenceDocument.CompanyId = 5;

            tblCompilenceDocument.DocumentOrder = null;
            tblCompilenceDocument.InsertDate = DateTime.Now;

            if(tblCompilenceDocument.DocumentAlias == null)
            {
                Uri uri = new Uri(tblCompilenceDocument.DocumentAliasLink);
                tblCompilenceDocument.DocumentName = System.IO.Path.GetFileName(uri.LocalPath);
                tblCompilenceDocument.DocumentAlias = "0";
            }
            else
            {

                tblCompilenceDocument.DocumentName = Path.GetFileName(tblCompilenceDocument.DocumentAlias);
                tblCompilenceDocument.DocumentAliasLink = "0";
            }

            _context.Add(tblCompilenceDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DocumentIndex));
        }

        #endregion Document
    }
}
