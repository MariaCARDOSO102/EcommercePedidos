using AutoMapper;
using EcommercePedidos.Objects.Dtos.Entities;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service;
using System.Runtime;

namespace EcommercePedidos.Objects.Dtos.Mappings
{
    public class PedidoMapping : Profile
    {
        public PedidoMapping()
        {
            CreateMap<PedidoDTO, Pedido>().ReverseMap();
        }
    }
}
