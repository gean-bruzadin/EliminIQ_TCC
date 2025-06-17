using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class QuizzController : Controller
    {
        private readonly DbConfig _dbConfig;

        public QuizzController(DbConfig dbConfig)
            => _dbConfig = dbConfig;



        // ------- DASHBOARD (permanece aqui) -------

    

        // ------- CRUD de Usuario (vai sempre para Dashboard) -------

        [HttpGet]
        public IActionResult CriarQuizz()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarQuizz(Quizz quizz)
        {
            if (quizz == null)
                return View(quizz);

            await _dbConfig.Quizz.AddAsync(quizz);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Alternativa");
        }

        [HttpGet]
        public async Task<IActionResult> EditarQuiz(int id)
        {

            var quizz = await _dbConfig.Quizz.FindAsync(id);
            if (quizz == null)
                return NotFound();

            return View(quizz);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {

            var quizz = await _dbConfig.Quizz.FindAsync(id);
            if (quizz == null)
                return NotFound();

            return View(quizz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarQuizz(Quizz quizz)
        {
            _dbConfig.Quizz.Update(quizz);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarQuizz(int id)
        {
            var quizz = await _dbConfig.Quizz.FindAsync(id);
            if (quizz != null)
            {
                _dbConfig.Quizz.Remove(quizz);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
