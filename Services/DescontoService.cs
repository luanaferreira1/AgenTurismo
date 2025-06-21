namespace AgenTurismo.Services
{
    public static class DescontoService
    {
        public delegate decimal CalculateDiscount(decimal precoOriginal);

        public static decimal AplicarDesconto10Porcento(decimal precoOriginal)
        {
            return precoOriginal * 0.9m;
        }

        public static decimal TestarDesconto(CalculateDiscount metodoDesconto, decimal preco)
        {
            return metodoDesconto(preco);
        }
    }
}
