using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class PerguntaController : Controller
    {
        private readonly DbConfig _db;

        public PerguntaController(DbConfig db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var perguntas = await _db.Pergunta
                .Include(p => p.Quizz)
                .ToListAsync();
            return View(perguntas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                _db.Pergunta.Add(pergunta);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pergunta);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pergunta = await _db.Pergunta
                .Include(p => p.Quizz)
                .FirstOrDefaultAsync(p => p.Id_Pergunta == id);
            if (pergunta == null)
                return NotFound();
            return View(pergunta);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pergunta = await _db.Pergunta.FindAsync(id);
            if (pergunta == null)
                return NotFound();
            return View(pergunta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                _db.Pergunta.Update(pergunta);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pergunta);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pergunta = await _db.Pergunta.FindAsync(id);
            if (pergunta == null)
                return NotFound();
            return View(pergunta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pergunta = await _db.Pergunta.FindAsync(id);
            _db.Pergunta.Remove(pergunta);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
