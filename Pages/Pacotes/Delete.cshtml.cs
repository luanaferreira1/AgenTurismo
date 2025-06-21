using AgenTurismo.Data;
using AgenTurismo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenTurismo.Pages.Pacotes
{
    [Authorize] 
    public class DeleteModel : PageModel
    {
        private readonly AgenciaContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(AgenciaContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PacoteTuristico = await _context.PacotesTuristicos
                .Include(p => p.Destinos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (PacoteTuristico == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            const int maxRetries = 3;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    var pacote = await _context.PacotesTuristicos.FindAsync(id);
                    if (pacote == null)
                    {
                        return NotFound();
                    }

                    pacote.IsDeleted = true;
                    pacote.DeletedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.SqliteErrorCode == 5)
                {
                    retryCount++;
                    if (retryCount >= maxRetries)
                        throw;

                    await Task.Delay(100 * retryCount);
                }
            }
            return Page();
        }
    }
}