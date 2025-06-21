using System.ComponentModel.DataAnnotations;
using AgenTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenTurismo.Pages.CidadesDestino
{
    public class CreateCidadeDestinoModel : PageModel
    {
        private readonly AgenTurismo.Data.AgenciaContext _context;

        public CreateCidadeDestinoModel(AgenTurismo.Data.AgenciaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CidadeDestinoInputModel Input { get; set; } = new CidadeDestinoInputModel();

        public class CidadeDestinoInputModel
        {
            [Required(ErrorMessage = "O nome da cidade é obrigatório.")]
            [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "O país é obrigatório.")]
            public string Pais { get; set; } = string.Empty;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var destino = new Destino
            {
                Nome = Input.Nome,
                Pais = Input.Pais
            };

            _context.Destinos.Add(destino);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cidade cadastrada com sucesso!";
            return Page();
        }
    }
}