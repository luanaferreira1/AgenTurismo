using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenTurismo.Models
{
    public class PacoteTuristico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100, MinimumLength = 5)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de início é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = "A capacidade máxima é obrigatória")]
        [Range(1, 100)]
        public int CapacidadeMaxima { get; set; } = 10;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000)]
        public decimal Preco { get; set; } = 1000m;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public List<Destino> Destinos { get; set; } = new List<Destino>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();

        public event Action<string> CapacityReached = delegate { };

        public void VerificarCapacidade(int reservasAtuais)
        {
            if (reservasAtuais >= CapacidadeMaxima)
            {
                CapacityReached?.Invoke($"ALERTA: Capacidade máxima de {CapacidadeMaxima} atingida!");
            }
        }
    }
}