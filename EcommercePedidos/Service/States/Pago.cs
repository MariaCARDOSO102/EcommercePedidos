using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Entities;
using EcommercePedidos.Service.Interfaces;

namespace EcommercePedidos.Service.States
{
    public class Pago : IEstadoPedido
    {
        private PedidoService _pedido;

        public Pago(PedidoService pedido)
        {
            _pedido = pedido;
            _pedido.StatusPedido = StatusPedido.Pago;
        }

        void IEstadoPedido.CancelarPedido()
        {
            _pedido.StatusPedido = new Cancelado(_pedido);
        }
        void IEstadoPedido.DespacharPedido()
        {
            _pedido.StatusPedido = new Enviado(_pedido);
        }
        void IEstadoPedido.SucessoAoPagar()
        {
            throw new Exception("O pedido já foi pago.");
        }

        }
}
