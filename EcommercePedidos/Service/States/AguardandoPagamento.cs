using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;

namespace EcommercePedidos.Service.States
{
    public class AguardandoPagamento : IEstadoPedido
    {
        private Pedido pedido;

        public AguardandoPagamento(Pedido pedido)
        {
            this.pedido = pedido;
        }

        public void SucessoAoPagar()
        {
            pedido.StatusPedido = StatusPedido.Pago;
        }

        public void CancelarPedido()
        {
            pedido.StatusPedido = StatusPedido.Cancelado;
        }

        public void DespacharPedido()
        {
            throw new InvalidOperationException("Não é possível despachar um pedido que ainda não foi pago.");
        }

        public Pedido GetPedido()
        {
            return pedido;
        }
    }
}
