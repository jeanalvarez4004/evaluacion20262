using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using evaluacion20262.Data;
using evaluacion20262.Models;

namespace evaluacion20262.Controllers;

public class SolicitudesController : Controller
{
    private readonly AppDbContext _context;
    public SolicitudesController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var lista = await _context.Solicitudes.OrderByDescending(s => s.FechaRegistro).ToListAsync();
        return View(lista);
    }

    [HttpGet]
    public IActionResult Crear() => View(new SolicitudServicio());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(SolicitudServicio model)
    {
        if (!ModelState.IsValid) return View(model);
        model.FechaRegistro = DateTime.Now;
        _context.Solicitudes.Add(model);
        await _context.SaveChangesAsync();
        TempData["Success"] = $"¡Solicitud registrada! Ticket #{model.Id} para {model.Cliente}";
        return RedirectToAction(nameof(Index));
    }
}
