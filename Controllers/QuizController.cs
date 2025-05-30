using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EliminIQ_TCC.Controllers
{
    public class QuizController : Controller
    {
        private readonly DbConfig _dbConfig;

        public QuizController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<ActionResult> BuscarPorId(int id)
        {
            var Quiz = await _dbConfig.Quiz.FindAsync(id);
            return View(Quiz);
        }

        public async Task<ActionResult> Listar_Quiz()
        {
            var Quiz = await _dbConfig.Quiz.ToListAsync();
            return View(Quiz);
        }

        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Criar_Quiz(Quiz quiz)
        {
            await _dbConfig.Quiz.AddAsync(quiz);
            await _dbConfig.SaveChangesAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Atualizar_Quiz(Quiz quiz)
        {
            _dbConfig.Quiz.Update(quiz);
            await _dbConfig.SaveChangesAsync();
            return View();
        }

        [HttpDelete]
        public async Task<ActionResult> Deletar_Quiz(int id)
        {
            var quiz = _dbConfig.Quiz.Find(id);
            _dbConfig.Quiz.Remove(quiz);
            await _dbConfig.SaveChangesAsync();
            return View();
        }

    }
}
