using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Entities;

namespace EcommercePedidos.Service.States
{
    public class Enviado : IEstadoPedido
    {
        public IEstadoPedido CancelarPedido()
        {
            throw new Exception("Operação não suportada, o pedido já foi enviado");
        }

        public IEstadoPedido DespacharPedido()
        {
            throw new Exception("Operação não suportada, o pedido já foi enviado");
        }

        public IEstadoPedido SucessoAoPagar()
        {
            throw new Exception("Operação não suportada, o pedido já foi enviado");
        }
    }
}
