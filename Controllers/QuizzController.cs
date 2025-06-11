using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class QuizzController : Controller
    {
        private readonly DbConfig _db;

        public QuizzController(DbConfig db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var quizzes = await _db.Quizz
                .Include(q => q.TipoQuizz)
                .Include(q => q.Dificuldade)
                .Include(q => q.Privacidade)
                .ToListAsync();
            return View(quizzes);
        }

        public IActionResult CriarQuizz()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarQuizz(Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                _db.Quizz.Add(quizz);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizz);
        }

        public async Task<IActionResult> DetalhesQuizz(int id)
        {
            var quizz = await _db.Quizz
                .Include(q => q.TipoQuizz)
                .Include(q => q.Dificuldade)
                .Include(q => q.Privacidade)
                .FirstOrDefaultAsync(q => q.Id_Quiz == id);
            if (quizz == null)
                return NotFound();
            return View(quizz);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var quizz = await _db.Quizz.FindAsync(id);
            if (quizz == null)
                return NotFound();
            return View(quizz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarQuizz(Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                _db.Quizz.Update(quizz);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizz);
        }

        public async Task<IActionResult> DeletarQuizz(int id)
        {
            var quizz = await _db.Quizz.FindAsync(id);
            if (quizz == null)
                return NotFound();
            return View(quizz);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarDeletar(int id)
        {
            var quizz = await _db.Quizz.FindAsync(id);
            _db.Quizz.Remove(quizz);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
