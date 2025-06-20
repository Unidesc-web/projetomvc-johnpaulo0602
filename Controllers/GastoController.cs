// Controllers/GastoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var gastos = _context.Gastos
                .Include(g => g.Categoria)
                .Include(g => g.Conta)
                .Include(g => g.Usuario)
                .OrderByDescending(g => g.Data);
            
            return View(await gastos.ToListAsync());
        }

        // GET: Gasto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var gasto = await _context.Gastos
                .Include(g => g.Categoria)
                .Include(g => g.Conta)
                .Include(g => g.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (gasto == null) return NotFound();

            return View(gasto);
        }

        // GET: Gasto/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["ContaId"] = new SelectList(_context.Contas, "Id", "Nome");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: Gasto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", gasto.CategoriaId);
            ViewData["ContaId"] = new SelectList(_context.Contas, "Id", "Nome", gasto.ContaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", gasto.UsuarioId);
            return View(gasto);
        }

        // GET: Gasto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null) return NotFound();
            
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", gasto.CategoriaId);
            ViewData["ContaId"] = new SelectList(_context.Contas, "Id", "Nome", gasto.ContaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", gasto.UsuarioId);
            return View(gasto);
        }

        // POST: Gasto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gasto gasto)
        {
            if (id != gasto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", gasto.CategoriaId);
            ViewData["ContaId"] = new SelectList(_context.Contas, "Id", "Nome", gasto.ContaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", gasto.UsuarioId);
            return View(gasto);
        }

        // GET: Gasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var gasto = await _context.Gastos
                .Include(g => g.Categoria)
                .Include(g => g.Conta)
                .Include(g => g.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (gasto == null) return NotFound();

            return View(gasto);
        }

        // POST: Gasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}