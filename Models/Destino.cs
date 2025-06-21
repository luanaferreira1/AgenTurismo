namespace AgenTurismo.Models
{
    public class Destino
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public List<PacoteTuristico> PacotesTuristicos { get; set; } = new List<PacoteTuristico>();
    }
}