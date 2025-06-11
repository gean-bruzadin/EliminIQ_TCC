using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class PrivacidadeController : Controller
    {
        private readonly DbConfig _db;

        public PrivacidadeController(DbConfig db)
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

        public IActionResult CriarPrivacidade()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarPrivacidade(Alternativa alternativa)
        {
            if (ModelState.IsValid)
            {
                _db.Alternativa.Add(alternativa);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alternativa);
        }

        public async Task<IActionResult> DetalhesPrivacidade(int id)
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
        public async Task<IActionResult> EditarPrivacidade(Alternativa alternativa)
        {
            if (ModelState.IsValid)
            {
                _db.Alternativa.Update(alternativa);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alternativa);
        }

        public async Task<IActionResult> DeletarPrivacidade(int id)
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
