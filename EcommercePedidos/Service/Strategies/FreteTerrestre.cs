namespace EcommercePedidos.Service.Strategies
{
    public class FreteTerrestre : IFrete
    {
        public double calcula(double Valor)
        {
            return Valor * 0.05;
        }
    }
}
