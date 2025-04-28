using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;

namespace EcommercePedidos.Service.States
{
    public class Cancelado : IEstadoPedido
    {
        private Pedido pedido;

        public Cancelado(Pedido pedido)
        {
            this.pedido = pedido;
        }

        public void SucessoAoPagar()
        {
            throw new InvalidOperationException("Não é possível pagar um pedido cancelado.");
        }

        public void CancelarPedido()
        {
            throw new InvalidOperationException("O pedido já está cancelado.");
        }

        public void DespacharPedido()
        {
            throw new InvalidOperationException("Não é possível despachar um pedido cancelado.");
        }

        public Pedido GetPedido()
        {
            return _pedido;
        }
    }
}
