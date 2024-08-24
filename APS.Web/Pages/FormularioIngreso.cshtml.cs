using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using APS.Data.Models;

namespace APS.Web.Pages
{
    public class FormularioIngresoModel : PageModel
    {
        private readonly APS.Data.Models.ApdatadbContext _context;

        public FormularioIngresoModel(APS.Data.Models.ApdatadbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Equipo Equipo { get; set; } = default!;

        // Método para manejar la solicitud POST cuando se envía el formulario.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Imprimir los errores de validación en el Debug.
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine("Validation error: " + error.ErrorMessage);
                    }
                }
                // Responder con JSON en caso de errores de validación
                return new JsonResult(new { success = false, message = "Datos de formulario no válidos." });
            }

            try
            {
                // Intenta insertar el equipo en la base de datos
                _context.Equipos.Add(Equipo);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("Equipo insertado exitosamente en la base de datos.");

                // Responder con JSON en caso de éxito
                return new JsonResult(new { success = true, message = "¡Equipo insertado exitosamente!" });
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción y maneja el error
                System.Diagnostics.Debug.WriteLine("Error al insertar en la base de datos: " + ex.Message);
                // Responder con JSON en caso de error
                return new JsonResult(new { success = false, message = "Ocurrió un error al insertar el equipo. Por favor, inténtelo de nuevo." });
            }
        }
    }
}
