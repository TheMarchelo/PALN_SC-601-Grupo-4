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

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Equipos.Add(Equipo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
