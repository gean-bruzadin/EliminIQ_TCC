using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class DificuldadeController : Controller
    {
        private readonly DbConfig _dbConfig;

        public DificuldadeController(DbConfig dbConfig)
            => _dbConfig = dbConfig;



        // ------- DASHBOARD (permanece aqui) -------

     
        // ------- CRUD de Usuario (vai sempre para Dashboard) -------

        [HttpGet]
        public IActionResult CriarDificudade()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarDificuldade(Dificuldade dificuldade)
        {
            if (dificuldade == null)
                return View(dificuldade);

            await _dbConfig.Dificuldade.AddAsync(dificuldade);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Dificuldade");
        }

        [HttpGet]
        public async Task<IActionResult> EditarDificuldade(int id)
        {

            var dificuldade = await _dbConfig.Dificuldade.FindAsync(id);
            if (dificuldade == null)
                return NotFound();

            return View(dificuldade);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {

            var dificuldade = await _dbConfig.Dificuldade.FindAsync(id);
            if (dificuldade == null)
                return NotFound();

            return View(dificuldade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarDificuldade(Dificuldade dificuldade)
        {
            _dbConfig.Dificuldade.Update(dificuldade);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarDificuldade(int id)
        {
            var dificuldade = await _dbConfig.Dificuldade.FindAsync(id);
            if (dificuldade != null)
            {
                _dbConfig.Dificuldade.Remove(dificuldade);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
