using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.WebApp.Controllers;

[Route("/")]
public class PaginaInicialController : Controller
{
    public IActionResult PaginaInicial()
    {
        return View("PaginaInicial");
    }
}
