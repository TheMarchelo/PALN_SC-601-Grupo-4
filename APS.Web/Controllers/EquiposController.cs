using Microsoft.AspNetCore.Mvc;
using APS.Data.Models;
using APS.Data;

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
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                Console.WriteLine(error); // O registra los errores
            }

            // Punto de interrupción aquí para ver el estado de ModelState y los datos de 'equipo'
            if (ModelState.IsValid)
            {
                // Solo asignar UsuarioId si no está definido y es necesario
                if (!equipo.UsuarioId.HasValue)
                {
                    equipo.UsuarioId = 1; // O asignar según tu lógica
                }

                _context.Equipos.Add(equipo);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }


            return View(equipo);
        }


        // Otros métodos del controlador...
    }
}
