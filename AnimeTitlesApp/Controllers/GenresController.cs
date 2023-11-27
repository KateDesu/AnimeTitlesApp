using AnimeTitlesApp.Models;
using AnimeTitlesApp.Models.Data;
using AnimeTitlesApp.ViewModels.Genres;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeTitlesApp.Controllers
{
    public class GenresController : Controller
    {
        private readonly AppCtx _context;

        public GenresController(AppCtx context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            // через контекст данных получаем доступ к таблице базы данных FormsOfStudy
            var appCtx = _context.Genres
                .OrderBy(f => f.GenreName);          // сортируем все записи по имени форм обучения

            // возвращаем в представление полученный список записей
            return View(await appCtx.ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGenreViewModel model)
        {
            if (_context.Genres
                .Where(f => f.GenreName == model.GenreName)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный жанр уже существует");
            }

            if (ModelState.IsValid)
            {
                Genre genre = new()
                {
                    GenreName = model.GenreName
                };

                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            EditGenreViewModel model = new()
            {
                Id = genre.Id,
                GenreName = genre.GenreName
            };
            return View(model);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditGenreViewModel model)
        {
            Genre genre = await _context.Genres.FindAsync(id);

            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    genre.GenreName = model.GenreName;
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'AppCtx.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(short id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
