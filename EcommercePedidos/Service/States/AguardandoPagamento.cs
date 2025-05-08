using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Service.Entities;

namespace EcommercePedidos.Service.States
{
    public class AguardandoPagamento : IEstadoPedido
    {
        public IEstadoPedido CancelarPedido()
        {
            return new Cancelado();
        }

        public IEstadoPedido DespacharPedido()
        {
            throw new Exception("Operação não suportada, " +
                "o pedido ainda não foi pago");
        }

        public IEstadoPedido SucessoAoPagar()
        {
            return new Pago();
        }
    }
}
