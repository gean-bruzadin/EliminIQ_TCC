using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class PrivacidadeController : Controller
    {
        private readonly DbConfig _dbConfig;

        public PrivacidadeController(DbConfig dbConfig)
            => _dbConfig = dbConfig;



        // ------- DASHBOARD (permanece aqui) -------

        
        // ------- CRUD de Usuario (vai sempre para Dashboard) -------

        [HttpGet]
        public IActionResult CriarPrivacidade()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarPrivacidade(Privacidade privacidade)
        {
            if (privacidade == null)
                return View(privacidade);

            await _dbConfig.Privacidade.AddAsync(privacidade);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Alternativa");
        }

        [HttpGet]
        public async Task<IActionResult> EditarPrivacidade(int id)
        {

            var privacidade = await _dbConfig.Privacidade.FindAsync(id);
            if (privacidade == null)
                return NotFound();

            return View(privacidade);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {

            var privacidade = await _dbConfig.Privacidade.FindAsync(id);
            if (privacidade == null)
                return NotFound();

            return View(privacidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarPrivacidade(Privacidade privacidade)
        {
            _dbConfig.Privacidade.Update(privacidade);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarPrivacidade(int id)
        {
            var privacidade = await _dbConfig.Privacidade.FindAsync(id);
            if (privacidade != null)
            {
                _dbConfig.Privacidade.Remove(privacidade);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
