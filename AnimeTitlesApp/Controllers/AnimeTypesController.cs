using AnimeTitlesApp.Models;
using AnimeTitlesApp.Models.Data;
using AnimeTitlesApp.ViewModels.AnimeTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeTitlesApp.Controllers
{
    public class AnimeTypesController : Controller
    {
        private readonly AppCtx _context;

        public AnimeTypesController(AppCtx context)
        {
            _context = context;
        }

        // GET: AnimeTypes
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.AnimeTypes
                .OrderBy(f => f.AnimeOfType);

            return _context.AnimeTypes != null ? 
                          View(await appCtx.ToListAsync()) :
                          Problem("Entity set 'AppCtx.AnimeTypes'  is null.");
        }

        // GET: AnimeTypes/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.AnimeTypes == null)
            {
                return NotFound();
            }

            var animeType = await _context.AnimeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeType == null)
            {
                return NotFound();
            }

            return View(animeType);
        }

        // GET: AnimeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimeTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnimeTypeViewModel model)
        {
            if (_context.AnimeTypes
                .Where(f => f.AnimeOfType == model.AnimeOfType)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный тип аниме уже существует");
            }

            if (ModelState.IsValid)
            {
                AnimeType animeType = new()
                {
                    AnimeOfType = model.AnimeOfType
                };

                _context.Add(animeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: AnimeTypes/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.AnimeTypes == null)
            {
                return NotFound();
            }

            var animeType = await _context.AnimeTypes.FindAsync(id);
            if (animeType == null)
            {
                return NotFound();
            }

            EditAnimeTypeViewModel model = new()
            {
                Id = animeType.Id,
                AnimeOfType = animeType.AnimeOfType
            };

            return View(model);
        }

        // POST: AnimeTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditAnimeTypeViewModel model)
        {
            if (_context.AnimeTypes
                .Where(f => f.AnimeOfType == model.AnimeOfType)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный тип аниме уже существует");
            }

            AnimeType animeType = await _context.AnimeTypes.FindAsync(id);

            if (id != animeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    animeType.AnimeOfType = model.AnimeOfType;
                    _context.Update(animeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeTypeExists(animeType.Id))
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
            return View(model);
        }

        // GET: AnimeTypes/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.AnimeTypes == null)
            {
                return NotFound();
            }

            var animeType = await _context.AnimeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeType == null)
            {
                return NotFound();
            }

            return View(animeType);
        }

        // POST: AnimeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.AnimeTypes == null)
            {
                return Problem("Entity set 'AppCtx.AnimeTypes'  is null.");
            }
            var animeType = await _context.AnimeTypes.FindAsync(id);
            if (animeType != null)
            {
                _context.AnimeTypes.Remove(animeType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeTypeExists(short id)
        {
          return (_context.AnimeTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
