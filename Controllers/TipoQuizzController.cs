using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class TipoQuizzController : Controller
    {
        private readonly DbConfig _dbConfig;

        public TipoQuizzController(DbConfig dbConfig)
            => _dbConfig = dbConfig;



        // ------- DASHBOARD (permanece aqui) -------

        public IActionResult Dashboard()
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            ViewBag.Nome = HttpContext.Session.GetString("UsuarioNome");
            return View();
        }

        // ------- CRUD de Usuario (vai sempre para Dashboard) -------

        [HttpGet]
        public IActionResult CriarTipoQuizz()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarTipoQuizz(TipoQuizz tipoquizz)
        {
            if (tipoquizz == null)
                return View(tipoquizz);

            await _dbConfig.TipoQuizz.AddAsync(tipoquizz);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Alternativa");
        }

        [HttpGet]
        public async Task<IActionResult> EditarTipoQuiz(int id)
        {

            var tipoquizz = await _dbConfig.TipoQuizz.FindAsync(id);
            if (tipoquizz == null)
                return NotFound();

            return View(tipoquizz);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {

            var tipoquizz = await _dbConfig.TipoQuizz.FindAsync(id);
            if (tipoquizz == null)
                return NotFound();

            return View(tipoquizz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarTipoQuizz(TipoQuizz tipoquizz)
        {
            _dbConfig.TipoQuizz.Update(tipoquizz);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarTipoQuizz(int id)
        {
            var tipoquizz = await _dbConfig.TipoQuizz.FindAsync(id);
            if (tipoquizz != null)
            {
                _dbConfig.TipoQuizz.Remove(tipoquizz);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
