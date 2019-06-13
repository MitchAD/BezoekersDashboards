using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BezoekersDashboard.Models;

namespace BezoekersDashboard.Controllers
{
    public class Roos24Controller : Controller
    {
        private readonly BezoekersDashboardContext _context;

        public Roos24Controller(BezoekersDashboardContext context)
        {
            _context = context;
        }

        // GET: Roos24
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roos24.ToListAsync());
        }

        // GET: Roos24/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roos24 = await _context.Roos24
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roos24 == null)
            {
                return NotFound();
            }

            return View(roos24);
        }

        // GET: Roos24/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roos24/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sponsors,Transactions,Recieved_Points,Exchanche_Points,Date")] Roos24 roos24)
        {
             
            if (ModelState.IsValid)
            {
                _context.Add(roos24);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roos24);
        }

        // GET: Roos24/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roos24 = await _context.Roos24.FindAsync(id);
            if (roos24 == null)
            {
                return NotFound();
            }
            return View(roos24);
        }

        // POST: Roos24/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Transactions,Recieved_Points,Exchanche_Points,Date")] Roos24 roos24)
        {
            if (id != roos24.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roos24);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Roos24Exists(roos24.Id))
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
            return View(roos24);
        }

        // GET: Roos24/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roos24 = await _context.Roos24
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roos24 == null)
            {
                return NotFound();
            }

            return View(roos24);
        }

        // POST: Roos24/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roos24 = await _context.Roos24.FindAsync(id);
            _context.Roos24.Remove(roos24);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Roos24Exists(int id)
        {
            return _context.Roos24.Any(e => e.Id == id);
        }
    }
}
