using EcommercePedidos.Objects.Models;

namespace EcommercePedidos.Service.Interfaces
{
    public interface IEstadoPedido
    {
        public void SucessoAoPagar();
        public void CancelarPedido();
        public void DespacharPedido();

    }
}
