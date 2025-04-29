using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Service.Entities;
using EcommercePedidos.Service.Interfaces;

namespace EcommercePedidos.Service.States
{
    public class Cancelado : IEstadoPedido
    {
        private PedidoService _pedido;

        public Cancelado(PedidoService pedido)
        {
            _pedido = pedido;
            _pedido.StatusPedido = StatusPedido.Cancelado;
        }

        void IEstadoPedido.CancelarPedido()
        {
            throw new Exception("Operação não suportada, pedido foi cancelado.");
        }
        void IEstadoPedido.DespacharPedido()
        {
            throw new Exception("Operação não suportada, pedido foi cancelado.");
        }
        void IEstadoPedido.SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, pedido foi cancelado.");
        }
    }
}
