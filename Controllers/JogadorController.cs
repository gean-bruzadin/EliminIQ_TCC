using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            RedirectToAction("Login", "Jogador");

        // ------- LOGIN / LOGOUT -------

        [HttpGet]
        public IActionResult Login()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var jogador = await _dbConfig.Jogador
                .FirstOrDefaultAsync(j =>
                    j.Email_Jogador == email.Trim() &&
                    j.Senha_Jogador == senha.Trim());

            if (jogador == null)
            {
                ViewBag.Erro = "Email ou senha inválidos.";
                return View();
            }

            HttpContext.Session.SetInt32("JogadorId", jogador.Id_Jogador);
            HttpContext.Session.SetString("JogadorNome", jogador.Nome_Jogador);
            return RedirectToAction("Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            if (!UsuarioLogado()) return RedirecionarAoLogin();
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
            if (!ModelState.IsValid)
                return View(jogador);

            await _dbConfig.Jogador.AddAsync(jogador);
            await _dbConfig.SaveChangesAsync();
            // após cadastro, manda pro Login
            return RedirectToAction("Login", "Jogador"); // especificar a controller junto com action
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!UsuarioLogado()) return RedirecionarAoLogin();
            var jogador = await _dbConfig.Jogador.FindAsync(id);
            if (jogador == null) return NotFound();
            return View(jogador);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            if (!UsuarioLogado()) return RedirecionarAoLogin();
            var jogador = await _dbConfig.Jogador.FindAsync(id);
            if (jogador == null) return NotFound();
            return View(jogador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(Jogador jogador)
        {
            if (!UsuarioLogado()) return RedirecionarAoLogin();
            if (!ModelState.IsValid)
                return View(jogador);

            _dbConfig.Jogador.Update(jogador);
            await _dbConfig.SaveChangesAsync();
            // volta para dashboard
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            if (!UsuarioLogado()) return RedirecionarAoLogin();
            var jogador = await _dbConfig.Jogador.FindAsync(id);
            if (jogador != null)
            {
                _dbConfig.Jogador.Remove(jogador);
                await _dbConfig.SaveChangesAsync();
            }
            // volta para dashboard
            return RedirectToAction("Dashboard");
        }
    }
}
