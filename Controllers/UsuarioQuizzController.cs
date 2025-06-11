using EliminIQ_TCC.Config;
using EliminIQ_TCC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EliminIQ_TCC.Controllers
{
    public class UsuarioQuizzController : Controller
    {
        private readonly DbConfig _dbConfig;

        public UsuarioQuizzController(DbConfig dbConfig)
            => _dbConfig = dbConfig;

        // Auxiliares de sessão (mesmo do UsuarioController)
        private bool UsuarioLogado() =>
            HttpContext.Session.GetInt32("UsuarioId") != null;

        private IActionResult RedirecionarAoLogin() =>
            RedirectToAction("Login", "Auth");

        // ------- LISTAGEM (Index) -------

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            // Carrega todos os registros; você pode incluir joins se quiser exibir dados relacionados
            var lista = await _dbConfig.Usuario_Quizz
                .AsNoTracking()
                .ToListAsync();

            return View(lista);
        }

        // ------- DETALHES -------

        [HttpGet]
        public async Task<IActionResult> Detalhes(int? fk_usuario, int? fk_quizz)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (fk_usuario == null || fk_quizz == null)
                return BadRequest();

            var item = await _dbConfig.Usuario_Quizz
                .AsNoTracking()
                .FirstOrDefaultAsync(uq => uq.Fk_Usuario == fk_usuario && uq.Fk_Quizz == fk_quizz);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // ------- CREATE (Cadastro) -------

        [HttpGet]
        public IActionResult Cadastro()
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            // Caso queira fornecer listas de Usuários ou Quizzes para seleção:
            // ViewBag.Usuarios = _dbConfig.Usuario.ToList();
            // ViewBag.Quizzes = _dbConfig.Quizz.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind("Fk_Usuario,Fk_Quizz,Vida,StatusVida,Respaw")] Usuario_Quizz usuarioQuizz)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (usuarioQuizz == null)
                return View(usuarioQuizz);

            // Verificar se já existe a combinação (chave composta)
            bool existe = await _dbConfig.Usuario_Quizz
                .AnyAsync(uq => uq.Fk_Usuario == usuarioQuizz.Fk_Usuario
                             && uq.Fk_Quizz == usuarioQuizz.Fk_Quizz);
            if (existe)
            {
                ModelState.AddModelError(string.Empty, "Já existe um registro para este Usuário e Quizz.");
                return View(usuarioQuizz);
            }

            if (ModelState.IsValid)
            {
                await _dbConfig.Usuario_Quizz.AddAsync(usuarioQuizz);
                await _dbConfig.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioQuizz);
        }

        // ------- EDIT (Atualizar) -------

        [HttpGet]
        public async Task<IActionResult> Atualizar(int? fk_usuario, int? fk_quizz)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (fk_usuario == null || fk_quizz == null)
                return BadRequest();

            var item = await _dbConfig.Usuario_Quizz
                .FirstOrDefaultAsync(uq => uq.Fk_Usuario == fk_usuario && uq.Fk_Quizz == fk_quizz);

            if (item == null)
                return NotFound();

            // Caso precise de listas para dropdowns, configure aqui (sem alterar a chave composta a menos que queira permitir troca):
            // ViewBag.Usuarios = _dbConfig.Usuario.ToList();
            // ViewBag.Quizzes = _dbConfig.Quizz.ToList();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(int fk_usuario, int fk_quizz,
            [Bind("Fk_Usuario,Fk_Quizz,Vida,StatusVida,Respaw")] Usuario_Quizz usuarioQuizz)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (fk_usuario != usuarioQuizz.Fk_Usuario || fk_quizz != usuarioQuizz.Fk_Quizz)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(usuarioQuizz);

            // Atualiza
            try
            {
                _dbConfig.Usuario_Quizz.Update(usuarioQuizz);
                await _dbConfig.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool existe = await _dbConfig.Usuario_Quizz
                    .AnyAsync(uq => uq.Fk_Usuario == fk_usuario && uq.Fk_Quizz == fk_quizz);
                if (!existe)
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // ------- DELETE (Deletar) -------

        [HttpGet]
        public async Task<IActionResult> Deletar(int? fk_usuario, int? fk_quizz)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            if (fk_usuario == null || fk_quizz == null)
                return BadRequest();

            var item = await _dbConfig.Usuario_Quizz
                .AsNoTracking()
                .FirstOrDefaultAsync(uq => uq.Fk_Usuario == fk_usuario && uq.Fk_Quizz == fk_quizz);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int fk_usuario, int fk_quizz)
        {
            if (!UsuarioLogado())
                return RedirecionarAoLogin();

            var item = await _dbConfig.Usuario_Quizz
                .FindAsync(fk_usuario, fk_quizz);
            if (item != null)
            {
                _dbConfig.Usuario_Quizz.Remove(item);
                await _dbConfig.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
