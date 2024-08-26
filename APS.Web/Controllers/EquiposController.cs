using Microsoft.AspNetCore.Mvc;
using APS.Data.Models;
using APS.Data;
using System;

namespace APS.Web.Controllers
{
    public class EquiposController : Controller
    {
        private readonly ApdatadbContext _context;

        public EquiposController(ApdatadbContext context)
        {
            _context = context;
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                // Asignar un valor de UsuarioId si no está asignado
                if (!equipo.UsuarioId.HasValue)
                {
                    equipo.UsuarioId = 1; // Valor predeterminado o según lógica de negocio
                }

                try
                {
                    _context.Equipos.Add(equipo);
                    _context.SaveChanges(); // Guarda en la base de datos

                    // Redirige a la misma página con un parámetro para mostrar el modal de éxito
                    return RedirectToAction("Create", new { saved = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
                    // Puedes mostrar un mensaje de error al usuario o registrar el error
                }
            }

            // Si el modelo no es válido, vuelve a mostrar el formulario con los datos ingresados
            return View(equipo);
        }

        // Otros métodos del controlador...
    }
}
