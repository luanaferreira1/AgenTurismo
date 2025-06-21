using AgenTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenTurismo.Pages.Pacotes
{
    public class DetailsModel : PageModel
    {
        private readonly AgenTurismo.Data.AgenciaContext _context;

        public DetailsModel(AgenTurismo.Data.AgenciaContext context)
        {
            _context = context;
        }

        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PacotesTuristicos == null)
            {
                return NotFound();
            }

            var pacote = await _context.PacotesTuristicos
                .Include(p => p.Destinos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pacote == null)
            {
                return NotFound();
            }

            PacoteTuristico = pacote;
            return Page();
        }
    }
}