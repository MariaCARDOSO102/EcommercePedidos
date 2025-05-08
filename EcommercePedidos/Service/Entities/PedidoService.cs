using EcommercePedidos.Data.Interfaces;
using EcommercePedidos.Objects.Dtos.Entities;
using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Interfaces;
using EcommercePedidos.Service.States;

namespace EcommercePedidos.Service.Entities
{
    public class PedidoService : Pedido, IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _repository = pedidoRepository;
            Status = ObterStatusClasse();
        }

        public async Task<IEnumerable<PedidoDTO>> ListarTodos()
        {
            var pedidos = await _repository.Get();
            List<PedidoDTO> entitiesDTO = [];

            foreach (var entity in pedidos)
            {
                entitiesDTO.Add(ConverterParaDTO(entity));
            }

            return entitiesDTO;
        }

        public async Task<PedidoDTO> ObterPorId(int id)
        {
            var entity = await _repository.GetById(id);
            return ConverterParaDTO(entity);
        }

        public async Task GerarPedido(PedidoDTO entitiesDTO)
        {
            var entity = ConverterParaModel(entitiesDTO);
            await _repository.GerarPedido(entity);
        }

        public async Task Atualizar(PedidoDTO entitiesDTO, int id)
        {
            var existingEntity = await _repository.GetById(id);

            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            var entity = ConverterParaModel(entitiesDTO);
            await _repository.Update(entity);
        }

        public Task<PedidoDTO> SucessoAoPagar(PedidoDTO entitiesDTO)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> DespacharPedido(PedidoDTO entitiesDTO)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> CancelarPedido(PedidoDTO entitiesDTO)
        {
            throw new NotImplementedException();
        }

        public IEstadoPedido Status;

        private IEstadoPedido ObterStatusClasse()
        {
            return StatusPedido switch
            {
                StatusPedido.AguardandoPagamento => new AguardandoPagamento(),
                StatusPedido.Pago => new Pago(),
                StatusPedido.Enviado => new Enviado(),
                StatusPedido.Cancelado => new Cancelado(),
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

        private PedidoDTO ConverterParaDTO(Pedido pedido)
        {
            PedidoDTO pedidoDTO = new()
            {
                Id = pedido.Id,
                Produto = pedido.Produto,
                Valor = pedido.Valor,
                StatusPedido = (int)pedido.StatusPedido,
                TipoFrete = (int)pedido.TipoFrete,
            };

            return pedidoDTO;
        }

        private Pedido ConverterParaModel(PedidoDTO entitiesDTO)
        {
            Pedido pedido = new()
            {
                Id = entitiesDTO.Id,
                Valor = entitiesDTO.Valor,
                StatusPedido = (StatusPedido)entitiesDTO.StatusPedido,
                TipoFrete = (TipoFrete)entitiesDTO.TipoFrete,
            };

            return pedido;
        }
    }
}
