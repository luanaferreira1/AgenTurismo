using System.ComponentModel.DataAnnotations;
using AgenTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgenTurismo.Pages.Pacotes
{
    public class CreatePacoteTuristicoModel : PageModel
    {
        private readonly AgenTurismo.Data.AgenciaContext _context;

        public CreatePacoteTuristicoModel(AgenTurismo.Data.AgenciaContext context)
        {
            _context = context;
            DestinosDisponiveis = new SelectList(_context.Destinos.ToList(), "Id", "Nome");
        }

        [BindProperty]
        public PacoteTuristicoInputModel Input { get; set; } = new PacoteTuristicoInputModel();

        public SelectList DestinosDisponiveis { get; set; }

        public class PacoteTuristicoInputModel
        {
            [Required(ErrorMessage = "O título é obrigatório")]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 e 100 caracteres")]
            public string Titulo { get; set; } = string.Empty;

            [Required(ErrorMessage = "A data de início é obrigatória")]
            [DataType(DataType.Date)]
            public DateTime DataInicio { get; set; } = DateTime.Today.AddDays(7);

            [Required(ErrorMessage = "A capacidade máxima é obrigatória")]
            [Range(1, 100, ErrorMessage = "A capacidade deve ser entre 1 e 100")]
            public int CapacidadeMaxima { get; set; } = 10;

            [Required(ErrorMessage = "O preço é obrigatório")]
            [Range(0.01, 10000, ErrorMessage = "O preço deve ser positivo")]
            public decimal Preco { get; set; } = 1000m;

            [Required(ErrorMessage = "Selecione pelo menos um destino")]
            public List<int> DestinosSelecionados { get; set; } = new List<int>();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DestinosDisponiveis = new SelectList(_context.Destinos.ToList(), "Id", "Nome");
                return Page();
            }

            var pacote = new PacoteTuristico
            {
                Titulo = Input.Titulo,
                DataInicio = Input.DataInicio,
                CapacidadeMaxima = Input.CapacidadeMaxima,
                Preco = Input.Preco,
                Destinos = _context.Destinos.Where(d => Input.DestinosSelecionados.Contains(d.Id)).ToList()
            };

            _context.PacotesTuristicos.Add(pacote);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Pacote cadastrado com sucesso!";
            DestinosDisponiveis = new SelectList(_context.Destinos.ToList(), "Id", "Nome");
            return Page();
        }
    }
}