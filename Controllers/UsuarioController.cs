// Controllers/UsuarioController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Data;
using projetomvc_johnpaulo0602.Models;
using System.Text.Json;

namespace projetomvc_johnpaulo0602.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Contas)
                .Include(u => u.Gastos)
                .ToListAsync();

            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.Contas)
                    .ThenInclude(c => c.InstituicaoFinanceira)
                .Include(u => u.Gastos)
                    .ThenInclude(g => g.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuario/Create (JSON)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar se email já existe
                if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                {
                    return BadRequest("Email já está em uso");
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // POST: Usuario/Edit/5 (JSON)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] EditUsuarioModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("ID não confere");
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            // Verificar se email já existe em outro usuário
            if (await _context.Usuarios.AnyAsync(u => u.Email == model.Email && u.Id != id))
            {
                return BadRequest("Email já está em uso por outro usuário");
            }

            // Atualizar campos
            usuario.Nome = model.Nome;
            usuario.Email = model.Email;
            usuario.Telefone = model.Telefone;
            usuario.Cidade = model.Cidade;
            usuario.Estado = model.Estado;

            // Só atualizar senha se foi fornecida
            if (!string.IsNullOrEmpty(model.Senha))
            {
                usuario.Senha = model.Senha;
            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                    return NotFound();
                else
                    throw;
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Contas)
                .Include(u => u.Gastos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario != null)
            {
                // Verificar se há contas ou gastos associados
                if (usuario.Contas.Any() || usuario.Gastos.Any())
                {
                    return BadRequest("Não é possível excluir usuário com contas ou gastos associados");
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }

    // Model para edição (sem senha obrigatória)
    public class EditUsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Senha { get; set; } // Opcional na edição
        public string Telefone { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}