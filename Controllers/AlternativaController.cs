using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class AlternativaController : Controller
    {
        private readonly DbConfig _dbConfig;

        public AlternativaController(DbConfig dbConfig)
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
        public IActionResult CriarAlternativa()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarAlternativa(Alternativa alternativa)
        {
            if (alternativa == null)
                return View(alternativa);

            await _dbConfig.Alternativa.AddAsync(alternativa);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Alternativa");
        }

        [HttpGet]
        public async Task<IActionResult> EditarAlternativa(int id)
        {

            var alternativa = await _dbConfig.Alternativa.FindAsync(id);
            if (alternativa == null)
                return NotFound();

            return View(alternativa);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {

            var alternativa = await _dbConfig.Alternativa.FindAsync(id);
            if (alternativa == null)
                return NotFound();

            return View(alternativa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarAlternativa(Alternativa alternativa)
        {
            _dbConfig.Alternativa.Update(alternativa);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarAlternativa(int id)
        {
            var alternativa = await _dbConfig.Alternativa.FindAsync(id);
            if (alternativa != null)
            {
                _dbConfig.Alternativa.Remove(alternativa);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
