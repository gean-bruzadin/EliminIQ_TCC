using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class PerguntaController : Controller
    {
        private readonly DbConfig _dbConfig;

        public PerguntaController(DbConfig dbConfig)
            => _dbConfig = dbConfig;



        // ------- DASHBOARD (permanece aqui) -------



        // ------- CRUD de Usuario (vai sempre para Dashboard) -------

        [HttpGet]
        public IActionResult CriarPergunta()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarPergunta(Pergunta pergunta)
        {
            if (pergunta == null)
                return View(pergunta);

            await _dbConfig.Pergunta.AddAsync(pergunta);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Alternativa");
        }

        [HttpGet]
        public async Task<IActionResult> EditarPergunta(int id)
        {

            var pergunta = await _dbConfig.Pergunta.FindAsync(id);
            if (pergunta == null)
                return NotFound();

            return View(pergunta);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {

            var pergunta = await _dbConfig.Pergunta.FindAsync(id);
            if (pergunta == null)
                return NotFound();

            return View(pergunta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarPergunta(Pergunta pergunta)
        {
            _dbConfig.Pergunta.Update(pergunta);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarPergunta(int id)
        {
            var pergunta = await _dbConfig.Pergunta.FindAsync(id);
            if (pergunta != null)
            {
                _dbConfig.Pergunta.Remove(pergunta);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
