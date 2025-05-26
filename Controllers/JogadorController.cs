using AspNetCoreGeneratedDocument;
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

    

        public async Task<ActionResult> BuscarPorId(int id)
        {
            var Jogador = await _dbConfig.Jogador.FindAsync(id);
            return View("Detalhes", Jogador);
        }

        public async Task<ActionResult> Listar()
        {
            var Jogadores = await _dbConfig.Jogador.ToListAsync();
            return View(Jogadores);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Jogador jogador)
        {
            await _dbConfig.Jogador.AddAsync(jogador);
            await _dbConfig.SaveChangesAsync();
            return RedirectToAction("Listar");
        }

        [HttpPost]
        public async Task<ActionResult> Atualizar(Jogador jogador)
        {
            _dbConfig.Jogador.Update(jogador);
            await _dbConfig.SaveChangesAsync();
            return RedirectToRoute("Update");
        }

        [HttpDelete]
        public async Task<ActionResult> Deletar (int id)
        {
            var jogador = _dbConfig.Jogador.Find(id);
            _dbConfig.Jogador.Remove(jogador);
            await _dbConfig.SaveChangesAsync();
            return RedirectToRoute("Delete");

        }

    }



}

