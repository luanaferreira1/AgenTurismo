using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AgenTurismo.Data;
using AgenTurismo.Models;

namespace AgenTurismo.Pages.Pacotes
{
    public class EditModel : PageModel
    {
        private readonly AgenciaContext _context;

        public EditModel(AgenciaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; }

        public List<AssignedDestinoData> AssignedDestinosList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PacoteTuristico = await _context.PacotesTuristicos
                .Include(p => p.Destinos)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (PacoteTuristico == null)
            {
                return NotFound();
            }

            PopulateAssignedDestinoData(_context, PacoteTuristico);
            return Page();
        }

        public void PopulateAssignedDestinoData(AgenciaContext context, PacoteTuristico pacote)
        {
            var allDestinos = context.Destinos;
            var pacoteDestinos = new HashSet<int>(pacote.Destinos.Select(d => d.Id));
            AssignedDestinosList = new List<AssignedDestinoData>();
            foreach (var destino in allDestinos)
            {
                AssignedDestinosList.Add(new AssignedDestinoData
                {
                    DestinoID = destino.Id,
                    Nome = destino.Nome,
                    Assigned = pacoteDestinos.Contains(destino.Id)
                });
            }
        }

        public async Task<IActionResult> OnPostAsync(int id, string[] selectedDestinos)
        {
            var pacoteParaAtualizar = await _context.PacotesTuristicos
                .Include(p => p.Destinos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacoteParaAtualizar == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<PacoteTuristico>(
                pacoteParaAtualizar,
                "PacoteTuristico",
                p => p.Titulo, p => p.DataInicio, p => p.Preco, p => p.CapacidadeMaxima))
            {
                UpdatePacoteDestinos(selectedDestinos, pacoteParaAtualizar);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdatePacoteDestinos(selectedDestinos, pacoteParaAtualizar);
            PopulateAssignedDestinoData(_context, pacoteParaAtualizar);
            return Page();
        }

        private void UpdatePacoteDestinos(string[] selectedDestinos, PacoteTuristico pacoteToUpdate)
        {
            if (selectedDestinos == null)
            {
                pacoteToUpdate.Destinos = new List<Destino>();
                return;
            }

            var selectedDestinosHS = new HashSet<string>(selectedDestinos);
            var pacoteDestinos = new HashSet<int>(pacoteToUpdate.Destinos.Select(d => d.Id));

            foreach (var destino in _context.Destinos)
            {
                if (selectedDestinosHS.Contains(destino.Id.ToString()))
                {
                    if (!pacoteDestinos.Contains(destino.Id))
                    {
                        pacoteToUpdate.Destinos.Add(destino);
                    }
                }
                else
                {
                    if (pacoteDestinos.Contains(destino.Id))
                    {
                        var destinoToRemove = pacoteToUpdate.Destinos.FirstOrDefault(d => d.Id == destino.Id);
                        if (destinoToRemove != null)
                        {
                            pacoteToUpdate.Destinos.Remove(destinoToRemove);
                        }
                    }
                }
            }
        }
    }
}