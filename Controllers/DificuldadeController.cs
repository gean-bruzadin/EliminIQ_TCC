using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class DificuldadeController : Controller
    {
        private readonly DbConfig _db;

        public DificuldadeController(DbConfig db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var alternativas = await _db.Alternativa
                .Include(a => a.Pergunta)
                .ToListAsync();
            return View(alternativas);
        }

        public IActionResult CriarDificuldade()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarDificuldade(Alternativa alternativa)
        {
            if (ModelState.IsValid)
            {
                _db.Alternativa.Add(alternativa);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alternativa);
        }

        public async Task<IActionResult> DetalhesDificuldade(int id)
        {
            var alternativa = await _db.Alternativa
                .Include(a => a.Pergunta)
                .FirstOrDefaultAsync(a => a.Id_Alternativa == id);
            if (alternativa == null)
                return NotFound();
            return View(alternativa);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var alternativa = await _db.Alternativa.FindAsync(id);
            if (alternativa == null)
                return NotFound();
            return View(alternativa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarDificuldade(Alternativa alternativa)
        {
            if (ModelState.IsValid)
            {
                _db.Alternativa.Update(alternativa);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alternativa);
        }

        public async Task<IActionResult> DeletarDificuldade(int id)
        {
            var alternativa = await _db.Alternativa.FindAsync(id);
            if (alternativa == null)
                return NotFound();
            return View(alternativa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ComfirmarDeletar(int id)
        {
            var alternativa = await _db.Alternativa.FindAsync(id);
            _db.Alternativa.Remove(alternativa);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
