// Controllers/HomeController.cs
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetomvc_johnpaulo0602.Data;
using projetomvc_johnpaulo0602.Models;

namespace projetomvc_johnpaulo0602.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Buscar gastos com relacionamentos
        var gastos = await _context.Gastos
            .Include(g => g.Categoria)
            .Include(g => g.Conta)
            .Include(g => g.Usuario)
            .ToListAsync();

        return View(gastos);
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