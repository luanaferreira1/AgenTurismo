using AgenTurismo.Data;
using AgenTurismo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenTurismo.Pages.Pacotes
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly AgenciaContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AgenciaContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<PacoteTuristico> Pacotes { get; set; } = new List<PacoteTuristico>();

        public async Task OnGetAsync()
        {
            Pacotes = await _context.PacotesTuristicos
                .Where(p => !p.IsDeleted)
                .Include(p => p.Destinos)
                .Include(p => p.Reservas)
                .ToListAsync();
        }
    }
}