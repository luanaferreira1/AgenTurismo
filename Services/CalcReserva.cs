namespace AgenTurismo.Services
{
    public class CalcReserva
    {
        public static Func<int, decimal, decimal> CalcularPrecoTotal =
            (dias, precoDiaria) => dias * precoDiaria;

        public static decimal TestarCalculo(int dias, decimal precoDiaria)
        {
            return CalcularPrecoTotal(dias, precoDiaria);
        }
    }
}
