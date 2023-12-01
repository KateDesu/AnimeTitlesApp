using AnimeTitlesApp.Models;
using AnimeTitlesApp.Models.Data;
using AnimeTitlesApp.ViewModels.AnimeTitles;
using AnimeTitlesApp.ViewModels.AnimeTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AnimeTitlesApp.Controllers
{
    public class AnimeTitlesController : Controller
    {
        private readonly AppCtx _context;

        public AnimeTitlesController(AppCtx context)
        {
            _context = context;
        }

        // GET: AnimeTitles
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.AnimeTitles
                .Include(a => a.AnimeType)
                .OrderByDescending(o => o.YearOfIssue)
                .OrderBy(o => o.TitleName);

            return View(await appCtx.ToListAsync());
        }

        // GET: AnimeTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimeTitles == null)
            {
                return NotFound();
            }

            var animeTitle = await _context.AnimeTitles
                .Include(a => a.AnimeType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeTitle == null)
            {
                return NotFound();
            }

            return View(animeTitle);
        }

        // GET: AnimeTitles/Create
        public IActionResult Create()
        {
            ViewData["IdAnimeTitle"] = new SelectList(_context.AnimeTypes.OrderBy(o=>o.AnimeOfType), "Id", "AnimeOfType");
            return View();
        }

        // POST: AnimeTitles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnimeTitleViewModel model)
        {
            if (_context.AnimeTitles
                .Where(f => f.OrigName == model.OrigName &&
                    f.TitleName == model.TitleName &&
                    f.YearOfIssue == model.YearOfIssue &&
                    f.Descr == model.Descr &&
                    f.Poster == model.Poster &&
                    f.CountSeries == model.CountSeries &&
                    f.Duration == model.Duration &&
                    f.IsComplete == model.IsComplete &&
                    f.Studio == model.Studio &&
                    f.IdAnimeTitle == model.IdAnimeTitle)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеное аниме уже существует");
            }

            if (ModelState.IsValid)
            {
                AnimeTitle animeTitle = new()
                {
                    OrigName = model.OrigName,
                    TitleName = model.TitleName,
                    YearOfIssue = model.YearOfIssue,
                    Descr = model.Descr,
                    Poster = model.Poster,
                    CountSeries = model.CountSeries,
                    Duration = model.Duration,
                    IsComplete = model.IsComplete,
                    Studio = model.Studio,
                    IdAnimeTitle = model.IdAnimeTitle
                };

                _context.Add(animeTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAnimeTitle"] = new SelectList(_context.AnimeTypes.OrderBy(o => o.AnimeOfType), "Id", "AnimeOfType", model.IdAnimeTitle);
            return View(model);
        }

        // GET: AnimeTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimeTitles == null)
            {
                return NotFound();
            }

            var animeTitle = await _context.AnimeTitles.FindAsync(id);
            if (animeTitle == null)
            {
                return NotFound();
            }
            ViewData["IdAnimeTitle"] = new SelectList(_context.AnimeTypes, "Id", "AnimeOfType", animeTitle.IdAnimeTitle);
            return View(animeTitle);
        }

        // POST: AnimeTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrigName,TitleName,YearOfIssue,Descr,Poster,CountSeries,Duration,IsComplete,Studio,IdAnimeTitle")] AnimeTitle animeTitle)
        {
            if (id != animeTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animeTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeTitleExists(animeTitle.Id))
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
            ViewData["IdAnimeTitle"] = new SelectList(_context.AnimeTypes, "Id", "AnimeOfType", animeTitle.IdAnimeTitle);
            return View(animeTitle);
        }

        // GET: AnimeTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimeTitles == null)
            {
                return NotFound();
            }

            var animeTitle = await _context.AnimeTitles
                .Include(a => a.AnimeType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeTitle == null)
            {
                return NotFound();
            }

            return View(animeTitle);
        }

        // POST: AnimeTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimeTitles == null)
            {
                return Problem("Entity set 'AppCtx.AnimeTitles'  is null.");
            }
            var animeTitle = await _context.AnimeTitles.FindAsync(id);
            if (animeTitle != null)
            {
                _context.AnimeTitles.Remove(animeTitle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeTitleExists(int id)
        {
          return (_context.AnimeTitles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
