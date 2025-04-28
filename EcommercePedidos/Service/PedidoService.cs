using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.States;

namespace EcommercePedidos.Service
{
    public class PedidoService : IEstadoPedido
    {
        public Pedido Pedido { get; private set; }

        public PedidoService(Pedido pedido)
        {
            Pedido = pedido ?? throw new ArgumentNullException(nameof(pedido), "Pedido não pode ser nulo.");
        }

        public void SucessoAoPagar()
        {
            if (Pedido.StatusPedido != StatusPedido.AguardandoPagamento)
            {
                throw new InvalidOperationException($"Pedido não pode ser pago. Status atual: {Pedido.StatusPedido}");
            }

            Pedido.StatusPedido = StatusPedido.Pago;
        }

        public void DespacharPedido()
        {
            if (Pedido.StatusPedido != StatusPedido.Pago)
            {
                throw new InvalidOperationException($"Pedido só pode ser enviado se estiver pago. Status atual: {Pedido.StatusPedido}");
            }

            Pedido.StatusPedido = StatusPedido.Enviado;
        }

        public void CancelarPedido()
        {
            if (Pedido.StatusPedido == StatusPedido.Pago ||
                Pedido.StatusPedido == StatusPedido.Enviado ||
                Pedido.StatusPedido == StatusPedido.Cancelado)
            {
                throw new InvalidOperationException($"Pedido não pode ser cancelado neste status: {Pedido.StatusPedido}");
            }

            Pedido.StatusPedido = StatusPedido.Cancelado;
        }

        public Pedido GetPedido()
        {
            return Pedido;
        }

    }
}
