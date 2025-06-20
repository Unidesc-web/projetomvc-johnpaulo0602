// Controllers/ContaController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Data;
using projetomvc_johnpaulo0602.Models;

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
            var contas = _context.Contas
                .Include(c => c.InstituicaoFinanceira)
                .Include(c => c.Usuario);
            return View(await contas.ToListAsync());
        }

        // GET: Conta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var conta = await _context.Contas
                .Include(c => c.InstituicaoFinanceira)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (conta == null) return NotFound();

            return View(conta);
        }

        // GET: Conta/Create
        public IActionResult Create()
        {
            ViewData["InstituicaoFinanceiraId"] = new SelectList(_context.InstituicoesFinanceiras, "Id", "Nome");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: Conta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Conta conta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituicaoFinanceiraId"] = new SelectList(_context.InstituicoesFinanceiras, "Id", "Nome", conta.InstituicaoFinanceiraId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", conta.UsuarioId);
            return View(conta);
        }

        // GET: Conta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var conta = await _context.Contas.FindAsync(id);
            if (conta == null) return NotFound();
            
            ViewData["InstituicaoFinanceiraId"] = new SelectList(_context.InstituicoesFinanceiras, "Id", "Nome", conta.InstituicaoFinanceiraId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", conta.UsuarioId);
            return View(conta);
        }

        // POST: Conta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Conta conta)
        {
            if (id != conta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaExists(conta.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstituicaoFinanceiraId"] = new SelectList(_context.InstituicoesFinanceiras, "Id", "Nome", conta.InstituicaoFinanceiraId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", conta.UsuarioId);
            return View(conta);
        }

        // GET: Conta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var conta = await _context.Contas
                .Include(c => c.InstituicaoFinanceira)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (conta == null) return NotFound();

            return View(conta);
        }

        // POST: Conta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conta = await _context.Contas.FindAsync(id);
            if (conta != null)
            {
                _context.Contas.Remove(conta);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaExists(int id)
        {
            return _context.Contas.Any(e => e.Id == id);
        }
    }
}