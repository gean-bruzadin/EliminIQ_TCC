using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EliminIQ_TCC.Controllers
{
    public class QuizzController : Controller
    {
        private readonly DbConfig _dbConfig;

        public QuizzController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<ActionResult> BuscarPorId(int id)
        {
            var Quizz = await _dbConfig.Quiz.FindAsync(id);
            return View(Quizz);
        }

        public async Task<ActionResult> Listar_Quizz()
        {
            var Quizz = await _dbConfig.Quiz.ToListAsync();
            return View(Quizz);
        }

        [HttpGet]
        public ActionResult Criar_Quizz()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Criar_Quizz(Quizz quiz)
        {
            await _dbConfig.Quizz.AddAsync(quizz);
            await _dbConfig.SaveChangesAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Atualizar_Quizz(Quizz quiz)
        {
            _dbConfig.Quizz.Update(quiz);
            await _dbConfig.SaveChangesAsync();
            return View();
        }

        [HttpDelete]
        public async Task<ActionResult> Deletar_Quizz(int id)
        {
            var quiz = _dbConfig.Quiz.Find(id);
            _dbConfig.Quizz.Remove(quiz);
            await _dbConfig.SaveChangesAsync();
            return View();
        }

    }
}
