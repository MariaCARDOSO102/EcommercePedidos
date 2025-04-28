using EcommercePedidos.Objects.Models;

namespace EcommercePedidos.Service.States
{
    public class Enviado : IEstadoPedido
    {
        private Pedido pedido;

        public Enviado(Pedido pedido)
        {
            this.pedido = pedido;
        }
        public void SucessoAoPagar()
        {
            throw new InvalidOperationException("Não é possível pagar um pedido já enviado.");
        }

        public void CancelarPedido()
        {
            throw new InvalidOperationException("Não é possível cancelar um pedido que já foi enviado.");
        }

        public void DespacharPedido()
        {
            throw new InvalidOperationException("O pedido já foi enviado. Não é possível despachar novamente.");
        }

        public Pedido GetPedido()
        {
            return pedido;
        }
    }
}
