using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgenTurismo.Data;
using AgenTurismo.Models;

namespace AgenTurismo.Pages.Pacotes
{
    public class CreateModel : PageModel
    {
        private readonly AgenciaContext _context;

        public CreateModel(AgenciaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; }

        public List<AssignedDestinoData> AssignedDestinosList { get; set; }

        public void PopulateAssignedDestinoData()
        {
            var allDestinos = _context.Destinos;
            AssignedDestinosList = new List<AssignedDestinoData>();
            foreach (var destino in allDestinos)
            {
                AssignedDestinosList.Add(new AssignedDestinoData
                {
                    DestinoID = destino.Id,
                    Nome = destino.Nome,
                    Assigned = false
                });
            }
        }

        public IActionResult OnGet()
        {
            PopulateAssignedDestinoData();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedDestinos)
        {
            var newPacote = new PacoteTuristico();

            if (await TryUpdateModelAsync<PacoteTuristico>(
                newPacote,
                "PacoteTuristico",
                p => p.Titulo, p => p.DataInicio, p => p.CapacidadeMaxima, p => p.Preco))
            {
                if (selectedDestinos != null)
                {
                    newPacote.Destinos = new List<Destino>();
                    foreach (var destId in selectedDestinos)
                    {
                        var destToAdd = await _context.Destinos.FindAsync(int.Parse(destId));
                        if (destToAdd != null)
                        {
                            newPacote.Destinos.Add(destToAdd);
                        }
                    }
                }

                _context.PacotesTuristicos.Add(newPacote);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedDestinoData();
            return Page();
        }
    }
}