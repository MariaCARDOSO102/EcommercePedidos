using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Entities;

namespace EcommercePedidos.Service.States
{
    public interface IEstadoPedido
    {
        IEstadoPedido SucessoAoPagar();
        IEstadoPedido DespacharPedido();
        IEstadoPedido CancelarPedido();

    }
}
