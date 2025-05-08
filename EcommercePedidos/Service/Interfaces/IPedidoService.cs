using EcommercePedidos.Objects.Dtos.Entities;

namespace EcommercePedidos.Service.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTO>> ListarTodos();
        Task<PedidoDTO> ObterPorId(int id);
        Task GerarPedido(PedidoDTO pedidoDTO);
        Task Atualizar(PedidoDTO pedidoDTO, int id);

        Task<PedidoDTO> SucessoAoPagar(PedidoDTO entitiesDTO);
        Task<PedidoDTO> DespacharPedido(PedidoDTO entitiesDTO);
        Task<PedidoDTO> CancelarPedido(PedidoDTO entitiesDTO);
    }
}
