using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class JogadorController : Controller
    {
        private readonly DbConfig _dbConfig;

        public JogadorController(DbConfig dbConfig)
            => _dbConfig = dbConfig;

        // Auxiliares de sessão
        private bool UsuarioLogado() =>
            HttpContext.Session.GetInt32("JogadorId") != null;

        private IActionResult RedirecionarAoLogin() =>
            RedirectToAction("Login", "Auth");
        // passa a apontar para AuthController em vez de JogadorController

        // ------- DASHBOARD (permanece aqui) -------

        public IActionResult Dashboard()
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            ViewBag.Nome = HttpContext.Session.GetString("JogadorNome");
            return View();
        }

        // ------- CRUD de Jogador (vai sempre para Dashboard) -------

        [HttpGet]
        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jogador jogador)
        {
            if (jogador == null)
                return View(jogador);

            await _dbConfig.Jogador.AddAsync(jogador);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var jogador = await _dbConfig.Jogador.FindAsync(id);
            if (jogador == null)
                return NotFound();

            return View(jogador);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var jogador = await _dbConfig.Jogador.FindAsync(id);
            if (jogador == null)
                return NotFound();

            return View(jogador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(Jogador jogador)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (!ModelState.IsValid)
                return View(jogador);

            _dbConfig.Jogador.Update(jogador);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var jogador = await _dbConfig.Jogador.FindAsync(id);
            if (jogador != null)
            {
                _dbConfig.Jogador.Remove(jogador);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
