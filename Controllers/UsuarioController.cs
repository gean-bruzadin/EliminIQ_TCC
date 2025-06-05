using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DbConfig _dbConfig;

        public UsuarioController(DbConfig dbConfig)
            => _dbConfig = dbConfig;

        // Auxiliares de sessão
        private bool UsuarioLogado() =>
            HttpContext.Session.GetInt32("UsuarioId") != null;

        private IActionResult RedirecionarAoLogin() =>
            RedirectToAction("Login", "Auth");
        // passa a apontar para AuthController em vez de UsuarioController

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
        public IActionResult Cadastro()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(Usuario usuario)
        {
            if (usuario == null)
                return View(usuario);

            await _dbConfig.Usuario.AddAsync(usuario);
            await _dbConfig.SaveChangesAsync();

            // Após cadastro, manda para Auth/Login
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var usuario = await _dbConfig.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var usuario = await _dbConfig.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(Usuario usuario)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (!ModelState.IsValid)
                return View(usuario);

            _dbConfig.Usuario.Update(usuario);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var usuario = await _dbConfig.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _dbConfig.Usuario.Remove(usuario);
                await _dbConfig.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
