﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory.Data;
using Laboratory.Models;

namespace Laboratory.Controllers
{
    public class ManagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manages
        public async Task<IActionResult> Index()
        {
              return _context.Manage != null ? 
                          View(await _context.Manage.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Manage'  is null.");
        }

        // GET: Manages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Manage == null)
            {
                return NotFound();
            }

            var manage = await _context.Manage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manage == null)
            {
                return NotFound();
            }

            return View(manage);
        }

        // GET: Manages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DailyRequestLimitation")] Manage manage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manage);
        }

        // GET: Manages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Manage == null)
            {
                return NotFound();
            }

            var manage = await _context.Manage.FindAsync(id);
            if (manage == null)
            {
                return NotFound();
            }
            return View(manage);
        }

        // POST: Manages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DailyRequestLimitation")] Manage manage)
        {
            if (id != manage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManageExists(manage.Id))
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
            return View(manage);
        }

        // GET: Manages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Manage == null)
            {
                return NotFound();
            }

            var manage = await _context.Manage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manage == null)
            {
                return NotFound();
            }

            return View(manage);
        }

        // POST: Manages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Manage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Manage'  is null.");
            }
            var manage = await _context.Manage.FindAsync(id);
            if (manage != null)
            {
                _context.Manage.Remove(manage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManageExists(int id)
        {
          return (_context.Manage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}