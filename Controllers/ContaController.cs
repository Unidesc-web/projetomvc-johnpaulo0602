// Controllers/ContaController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Data;
using projetomvc_johnpaulo0602.Models;
using System.Text.Json;

namespace projetomvc_johnpaulo0602.Controllers
{
    public class ContaController : Controller
    {
        private readonly AppDbContext _context;

        public ContaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Conta
        public async Task<IActionResult> Index()
        {
            // Verificar se está logado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var contas = await _context.Contas
                .Include(c => c.InstituicaoFinanceira)
                .Include(c => c.Usuario)
                .Include(c => c.Gastos)
                .Where(c => c.UsuarioId == usuarioId) // Filtrar por usuário logado
                .ToListAsync();

            // Apenas dados das instituições para os dropdowns nos modais
            ViewBag.InstituicoesFinanceiras = await _context.InstituicoesFinanceiras
                .Select(i => new { id = i.Id, nome = i.Nome })
                .ToListAsync();

            return View(contas);
        }

        // POST: Conta/Create (JSON)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Conta conta)
        {
            // Verificar se está logado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            // Definir usuário automaticamente
            conta.UsuarioId = usuarioId.Value;

            // Remover validações desnecessárias para relacionamentos
            ModelState.Remove("InstituicaoFinanceira");
            ModelState.Remove("Usuario");
            ModelState.Remove("Gastos");

            if (ModelState.IsValid)
            {
                _context.Add(conta);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: Conta/Edit/5 (JSON)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] Conta conta)
        {
            if (id != conta.Id)
            {
                return BadRequest("ID não confere");
            }

            // Verificar se está logado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            // Verificar se a conta pertence ao usuário logado
            var contaExistente = await _context.Contas.FindAsync(id);
            if (contaExistente == null || contaExistente.UsuarioId != usuarioId)
            {
                return NotFound();
            }

            // Definir usuário automaticamente
            conta.UsuarioId = usuarioId.Value;

            // Remover validações desnecessárias para relacionamentos
            ModelState.Remove("InstituicaoFinanceira");
            ModelState.Remove("Usuario");
            ModelState.Remove("Gastos");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaExists(conta.Id))
                        return NotFound();
                    else
                        throw;
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: Conta/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Verificar se está logado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var conta = await _context.Contas.FindAsync(id);
            if (conta != null && conta.UsuarioId == usuarioId) // Só permite deletar se for do usuário
            {
                _context.Contas.Remove(conta);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        private bool ContaExists(int id)
        {
            return _context.Contas.Any(e => e.Id == id);
        }
    }
}