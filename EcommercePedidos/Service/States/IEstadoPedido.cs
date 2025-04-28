using EcommercePedidos.Objects.Models;

namespace EcommercePedidos.Service.States
    {
        public interface IEstadoPedido
        {
            public void SucessoAoPagar();
            public void CancelarPedido();
            public void DespacharPedido();
            public Pedido GetPedido();

        }
    }
