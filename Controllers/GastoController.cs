// Controllers/GastoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Data;
using projetomvc_johnpaulo0602.Models;

namespace projetomvc_johnpaulo0602.Controllers
{
    public class GastoController : Controller
    {
        private readonly AppDbContext _context;

        public GastoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gasto
        public async Task<IActionResult> Index()
        {
            var gastos = await _context.Gastos
                .Include(g => g.Categoria)
                .Include(g => g.Conta)
                    .ThenInclude(c => c.InstituicaoFinanceira)
                .Include(g => g.Usuario)
                .OrderByDescending(g => g.Data)
                .ToListAsync();

            // Dados para os dropdowns nos modais
            ViewBag.Categorias = await _context.Categorias
                .Select(c => new { id = c.Id, nome = c.Nome })
                .ToListAsync();

            ViewBag.Contas = await _context.Contas
                .Select(c => new { id = c.Id, nome = c.Nome })
                .ToListAsync();

            return View(gastos);
        }

        // POST: Gasto/Create (JSON)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Gasto gasto)
        {
            // Remover validações desnecessárias para relacionamentos
            ModelState.Remove("Categoria");
            ModelState.Remove("Conta");
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: Gasto/Edit/5 (JSON)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return BadRequest("ID não confere");
            }

            // Remover validações desnecessárias para relacionamentos
            ModelState.Remove("Categoria");
            ModelState.Remove("Conta");
            ModelState.Remove("Usuario");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.Id))
                        return NotFound();
                    else
                        throw;
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: Gasto/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}