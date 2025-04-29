using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Service.Entities;
using EcommercePedidos.Service.Interfaces;

namespace EcommercePedidos.Service.States
{
    public class AguardandoPagamento : IEstadoPedido
    {
        private PedidoService _pedido;

        public AguardandoPagamento(PedidoService pedido)
        {
            _pedido = pedido;
            _pedido.StatusPedido = StatusPedido.AguardandoPagamento;
        }

        void IEstadoPedido.CancelarPedido()
        {
            _pedido.StatusPedido = new Cancelado(_pedido);
        }
        void IEstadoPedido.DespacharPedido()
        {
            throw new Exception("Não é possível despachar um pedido que ainda não foi pago.");
        }
        void IEstadoPedido.SucessoAoPagar()
        {
            _pedido.StatusPedido = new Pago(_pedido);
        }
    }
}
