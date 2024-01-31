using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Egzamin_Rectangle1.Data;
using Egzamin_Rectangle1.Models;

namespace Egzamin_Rectangle1.Controllers
{
    public class RectanglesController : Controller
    {
        private readonly Context _context;

        public RectanglesController(Context context)
        {
            _context = context;
        }

        // GET: Rectangles
        public async Task<IActionResult> Index()
        {
            var context = _context.Rectangle.Include(r => r.HeightUnit).Include(r => r.WidthUnit);
            return View(await context.ToListAsync());
        }

        // GET: Rectangles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rectangle = await _context.Rectangle
                .Include(r => r.HeightUnit)
                .Include(r => r.WidthUnit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rectangle == null)
            {
                return NotFound();
            }

            ViewData["Area"] = Math.Round(1.0D * rectangle.Height * rectangle.Width * rectangle.WidthUnit.Multiplier * rectangle.HeightUnit.Multiplier / 1_000_000, 6);

            return View(rectangle);
        }

        // GET: Rectangles/Create
        public IActionResult Create()
        {
            ViewData["HeightUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Name");
            ViewData["WidthUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Name");
            return View();
        }

        // POST: Rectangles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Width,WidthUnitId,Height,HeightUnitId")] Rectangle rectangle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rectangle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HeightUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Id", rectangle.HeightUnitId);
            ViewData["WidthUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Id", rectangle.WidthUnitId);
            return View(rectangle);
        }

        // GET: Rectangles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rectangle = await _context.Rectangle.FindAsync(id);
            if (rectangle == null)
            {
                return NotFound();
            }
            ViewData["HeightUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Name", rectangle.HeightUnitId);
            ViewData["WidthUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Name", rectangle.WidthUnitId);
            return View(rectangle);
        }

        // POST: Rectangles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Width,WidthUnitId,Height,HeightUnitId")] Rectangle rectangle)
        {
            if (id != rectangle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rectangle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RectangleExists(rectangle.Id))
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
            ViewData["HeightUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Name", rectangle.HeightUnitId);
            ViewData["WidthUnitId"] = new SelectList(_context.Set<Unit>(), "Id", "Name", rectangle.WidthUnitId);
            return View(rectangle);
        }

        // GET: Rectangles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rectangle = await _context.Rectangle
                .Include(r => r.HeightUnit)
                .Include(r => r.WidthUnit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rectangle == null)
            {
                return NotFound();
            }

            return View(rectangle);
        }

        // POST: Rectangles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rectangle = await _context.Rectangle.FindAsync(id);
            if (rectangle != null)
            {
                _context.Rectangle.Remove(rectangle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RectangleExists(int id)
        {
            return _context.Rectangle.Any(e => e.Id == id);
        }
    }
}
