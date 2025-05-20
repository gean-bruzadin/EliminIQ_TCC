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
        {
            _dbConfig = dbConfig;
        }


        // GET: /Jogador/Create
        [HttpGet]
        public IActionResult Create()
            => View();   // pega Views/Jogador/Create.cshtml

        // POST: /Jogador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Jogador jogador)
        {
            if (!ModelState.IsValid)
                return View(jogador);

            await _dbConfig.Jogador.AddAsync(jogador);
            await _dbConfig.SaveChangesAsync();

            // redireciona de volta ao form ou pra outra página
            return RedirectToAction("Create");
        }

        // remova ou transforme este método:
        // public IActionResult Index() { … }


    }
}
