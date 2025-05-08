using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Entities;

namespace EcommercePedidos.Service.States
{
    public class Pago : IEstadoPedido
    {
        public IEstadoPedido CancelarPedido()
        {
            return new Cancelado();
        }

        public IEstadoPedido DespacharPedido()
        {
            return new Enviado();
        }

        public IEstadoPedido SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, o pedido já foi pago");
        }

    }
}
