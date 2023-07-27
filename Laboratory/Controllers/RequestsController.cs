using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory.Data;
using Laboratory.Models;
using Laboratory.Data.Migrations;

namespace Laboratory.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index(string? college, string? studentstatus)
        {

            if (!string.IsNullOrEmpty(college) && !string.IsNullOrEmpty(studentstatus))
            {
                return View(await _context.Request.Where(r => r.College == college && r.StudentStatus == studentstatus).ToListAsync());
            }
            else if (!string.IsNullOrEmpty(college) || ! string.IsNullOrEmpty(studentstatus))
            {
                return View(await _context.Request.Where(r => r.College == college || r.StudentStatus == studentstatus).ToListAsync());
            }
            else
            {
                return _context.Request != null ?
                    View(await _context.Request.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Requests' is null");
            }

        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            var manage = _context.Manage.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (manage is null)
            {
                ViewBag.ErrorMessage = "You Need to Set the Limit in Manage Page";
                return View();
            }
            var limitDays = manage.Value;
            var dateTo = DateTime.Now.AddDays(30);    
           List<DateTime> avalibleDates = new List<DateTime>();
            for (var date = DateTime.Now; date <= dateTo; date = date.AddDays(1)) 
            { 
                if (date.DayOfWeek.ToString() == "Friday" || date.DayOfWeek.ToString() == "Saturday")
                {
                    continue;
                }
                var requestCount = _context.Request.Where(x => x.DateSelected.Date == date.Date).Count();
                if (requestCount >= limitDays)
                {
                    continue;
                 }
                avalibleDates.Add(date);
            }
            ViewBag.AvalibleDates = avalibleDates;   
            return View();
        }
        

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalId,UniversityNo,StudentStatus,College,FirstNameEn,FatherNameEn,GrandfatherNameEn,FamilyNameEn,FirstNameAr,FatherNameAr,GrandfatherNameAr,FamilyNameAr,Email,PhoneNo,BirthDate,MidecalfileNo,DateSelected")] Request request)
        {
            var manage = _context.Manage.Where(x => x.Name == "limitationDays").FirstOrDefault();  
            if (manage is null)
            {
                ViewBag.ErrorMessage = "You Need to Set the Limit in Manage Page";
                return View();
            }
            var limitDays = manage.Value;
            var requestCount = _context.Request.Where(x=>x.DateSelected == request.DateSelected).Count();
            if (requestCount >= limitDays)
            {
                ViewBag.ErrorMessage = "Sorry, The Limit of Request for this day is Reached";
                return View();
            }

            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
               //return RedirectToAction(nameof(Index));
                return RedirectToAction("Message");
            }
            return View(request);
        }

        public IActionResult Message()
        {
            return View();
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UniversityNo,StudentStatus,College,FirstNameEn,FatherNameEn,GrandfatherNameEn,FamilyNameEn,FirstNameAr,FatherNameAr,GrandfatherNameAr,FamilyNameAr,Email,PhoneNo,BirthDate,MidecalfileNo")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Request == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Request'  is null.");
            }
            var request = await _context.Request.FindAsync(id);
            if (request != null)
            {
                _context.Request.Remove(request);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
          return (_context.Request?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
