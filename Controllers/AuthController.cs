using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class AuthController : Controller
    {
        private readonly DbConfig _dbConfig;

        public AuthController(DbConfig dbConfig)
            => _dbConfig = dbConfig;

        [HttpGet]
        public IActionResult Login()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Erro = "Informe email e senha.";
                return View();
            }

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

            return RedirectToAction("Dashboard", "Jogador");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
