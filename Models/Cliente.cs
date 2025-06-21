using System.ComponentModel.DataAnnotations;
using AgenTurismo.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email em formato inválido")]
    public string Email { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public List<Reserva> Reservas { get; set; } = new List<Reserva>();
}