using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Data;
using projetomvc_johnpaulo0602.Models;
using System.Diagnostics;

namespace projetomvc_johnpaulo0602.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Verificar se está logado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Buscar gastos com relacionamentos apenas do usuário logado
            var gastos = await _context.Gastos
                .Include(g => g.Categoria)
                .Include(g => g.Conta)
                    .ThenInclude(c => c.InstituicaoFinanceira)
                .Include(g => g.Usuario)
                .Where(g => g.UsuarioId == usuarioId)
                .ToListAsync();

            // Dados para os dropdowns nos modais (apenas do usuário logado)
            ViewBag.Categorias = await _context.Categorias
                .Select(c => new { id = c.Id, nome = c.Nome })
                .ToListAsync();

            ViewBag.Contas = await _context.Contas
                .Where(c => c.UsuarioId == usuarioId) // Apenas contas do usuário
                .Select(c => new { id = c.Id, nome = c.Nome })
                .ToListAsync();

            return View(gastos);
        }

        // API para buscar gastos do mês (para o widget da sidebar)
        [HttpGet]
        public async Task<IActionResult> GetGastosDoMes()
        {
            // Verificar se está logado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { success = false });
            }

            try
            {
                var dataAtual = DateTime.Now;
                var gastosDoMes = await _context.Gastos
                    .Where(g => g.UsuarioId == usuarioId && 
                               g.Data.Month == dataAtual.Month && 
                               g.Data.Year == dataAtual.Year)
                    .SumAsync(g => g.Valor);

                var mesAtual = dataAtual.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR"));

                return Json(new { 
                    success = true, 
                    valor = gastosDoMes, 
                    mes = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1) 
                });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}