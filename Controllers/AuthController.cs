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

            var usuario = await _dbConfig.Usuario
                .FirstOrDefaultAsync(u =>
                    u.Email_Usuario == email.Trim() &&
                    u.Senha_Usuario == senha.Trim());

            if (usuario == null)
            {
                ViewBag.Erro = "Email ou senha inválidos.";
                return View();
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id_Usuario);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome_Usuario);

            return RedirectToAction("Dashboard", "Usuario");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
