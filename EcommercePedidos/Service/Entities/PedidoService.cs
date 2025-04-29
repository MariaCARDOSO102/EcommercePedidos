using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Interfaces;
using EcommercePedidos.Service.States;

namespace EcommercePedidos.Service.Entities
{
    public class PedidoService : Pedido, IEstadoPedido
    {
        public IEstadoPedido Status;

        public PedidoService()
        {
            Status = ObterStatusClasse();
        }

        private IEstadoPedido ObterStatusClasse()
        {
            return StatusPedido switch
            {
                StatusPedido.AguardandoPagamento => new AguardandoPagamento(this),
                StatusPedido.Pago => new Pago(this),
                StatusPedido.Enviado => new Enviado(this),
                StatusPedido.Cancelado => new Cancelado(this),
                _ => throw new ArgumentException("Estado inválido"),
            };
        }

        private StatusPedido ObterEstadoEnum()
        {
            return Status switch
            {
                AguardandoPagamento _ => StatusPedido.AguardandoPagamento,
                Pago _ => StatusPedido.Pago,
                Enviado _ => StatusPedido.Enviado,
                Cancelado _ => StatusPedido.Cancelado,
                _ => throw new ArgumentException("Estado inválido"),
            };
        }
    }
