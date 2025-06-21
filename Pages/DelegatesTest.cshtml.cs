using System.ComponentModel.DataAnnotations;
using AgenTurismo.Models;
using AgenTurismo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenTurismo.Pages
{
    public class DelegatesTestModel : PageModel
    {
        [BindProperty]
        public decimal PrecoOriginal { get; set; }

        [BindProperty]
        public decimal PrecoComDesconto { get; set; }

        [BindProperty]
        public int DiasReserva { get; set; }

        [BindProperty]
        public decimal PrecoDiaria { get; set; }

        [BindProperty]
        public decimal PrecoTotal { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A mensagem de log é obrigatória")]
        public string MensagemLog { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPostCalcularDesconto()
        {
            PrecoComDesconto = DescontoService.TestarDesconto(
                DescontoService.AplicarDesconto10Porcento,
                PrecoOriginal);
        }

        public void OnPostCalcularTotal()
        {
            PrecoTotal = CalcReserva.TestarCalculo(DiasReserva, PrecoDiaria);
        }

        public IActionResult OnPostLogMensagem()
        {
            if (string.IsNullOrWhiteSpace(MensagemLog))
            {
                ModelState.AddModelError("MensagemLog", "A mensagem não pode ser vazia");
                return Page();
            }

            LogService.TestarLogMulticast(MensagemLog!);
            return Page();
        }

        public void OnPostTestarCapacidade()
        {
            var pacote = new PacoteTuristico
            {
                Id = 1,
                Titulo = "Pacote Teste",
                CapacidadeMaxima = 15,
                Preco = 1000m
            };

            pacote.CapacityReached += (mensagem) =>
            {
                Console.WriteLine($"ALERTA: {mensagem}");
                LogService.TestarLogMulticast(mensagem);
            };

            pacote.VerificarCapacidade(15);
        }
    }
}