using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;

namespace EcommercePedidos.Service.States
{
    public class Pago : IEstadoPedido
    {
        private Pedido pedido;

        public Pago(Pedido pedido)
        {
            this.pedido = pedido;
        }

        public void SucessoAoPagar()
        {
            throw new InvalidOperationException("O pedido já foi pago.");
        }

        public void CancelarPedido()
        {
            if (pedido.StatusPedido == StatusPedido.Pago)
            {
                pedido.StatusPedido = StatusPedido.Cancelado;
            }
        }

        public void DespacharPedido()
        {
            pedido.StatusPedido = StatusPedido.Enviado;
        }

        public Pedido GetPedido()
        {
            return pedido;
        }
    }
}
