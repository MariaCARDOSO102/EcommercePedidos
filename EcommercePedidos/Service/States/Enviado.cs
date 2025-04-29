using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Entities;
using EcommercePedidos.Service.Interfaces;

namespace EcommercePedidos.Service.States
{
    public class Enviado : IEstadoPedido
    {
        private PedidoService _pedido;

        public Enviado(PedidoService pedido)
        {
            _pedido = pedido;
            _pedido.StatusPedido = StatusPedido.Enviado;
        }

        void IEstadoPedido.CancelarPedido()
        {
            throw new Exception("Não é possível cancelar um pedido que já foi enviado.");
        }
        void IEstadoPedido.SucessoAoPagar()
        {
            throw new Exception("Não é possível pagar um pedido já enviado.");
        }
        void IEstadoPedido.DespacharPedido()
        {
            throw new Exception("O pedido já foi enviado. Não é possível despachar novamente.");
        }
    }
}
