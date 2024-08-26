using APS.Data;
using APS.Data.Models;
using APS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace APS.Web.Controllers
{
    public class AprobacionesController : Controller
    {
        private readonly ApdatadbContext _context;
        private readonly ILogger<AprobacionesController> _logger;

        public AprobacionesController(ApdatadbContext context, ILogger<AprobacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Acción para mostrar el formulario de aprobaciones
        [HttpGet]
        public IActionResult Create()
        {
            var criteriosExistentes = _context.Aprobaciones
                .Select(a => new CriterioViewModel
                {
                    NombreCriterio = a.Criterio,
                    Cumple = a.Cumple
                })
                .ToList();

            // Mensajes de depuración
            _logger.LogInformation($"Total criterios obtenidos: {criteriosExistentes.Count}");
            foreach (var criterio in criteriosExistentes)
            {
                _logger.LogInformation($"Criterio: {criterio.NombreCriterio}, Cumple: {criterio.Cumple}");
            }

            var model = new AprobacionViewModel
            {
                Criterios = criteriosExistentes
            };

            return View("Create", model);
        }

        // Acción para manejar el envío del formulario
        [HttpPost]
        public IActionResult GuardarAprobaciones(AprobacionViewModel model)
        {
            // Validación manual sin ModelState.IsValid
            if (model.Criterios == null || model.Criterios.Count == 0)
            {
                ModelState.AddModelError("", "No se han enviado criterios válidos.");
                return View("Create", model); // Volver a mostrar la vista con el mensaje de error
            }

            // Verificar si todas las casillas están marcadas
            bool todasMarcadas = model.Criterios.All(c => c.Cumple);

            if (todasMarcadas)
            {
                // Redirigir a la acción 'Create' del controlador 'Equipos'
                return RedirectToAction("Create", "Equipos");
            }
            else
            {
                // Mostrar un mensaje de error y redirigir a la misma vista
                ModelState.AddModelError("", "Todas las aprobaciones deben ser cumplidas para continuar.");
                return View("Create", model);
            }
        }
    }
}
