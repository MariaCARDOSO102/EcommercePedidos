using EcommercePedidos.Objects.Dtos.Entities;

namespace EcommercePedidos.Service.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTO>> ListarTodos();
        Task<PedidoDTO> ObterPorId(int id);
        Task<PedidoDTO> GerarPedido(PedidoDTO pedidoDTO);
        Task<PedidoDTO> Atualizar(PedidoDTO pedidoDTO, int id);

        Task<PedidoDTO> SucessoAoPagar(PedidoDTO entitiesDTO); 
        Task<PedidoDTO> DespacharPedido(PedidoDTO entitiesDTO);
        Task<PedidoDTO> CancelarPedido(PedidoDTO entitiesDTO);
    }
}
