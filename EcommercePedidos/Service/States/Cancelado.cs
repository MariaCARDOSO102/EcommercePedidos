using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Service.Entities;

namespace EcommercePedidos.Service.States
{
    public class Cancelado : IEstadoPedido
    {
        public IEstadoPedido CancelarPedido()
        {
            throw new Exception("Operação não suportada, o pedido foi cancelado");
        }

        public IEstadoPedido DespacharPedido()
        {
            throw new Exception("Operação não suportada, o pedido foi cancelado");
        }

        public IEstadoPedido SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, o pedido foi cancelado");
        }
    }
}
