using EcommercePedidos.Data.Interfaces;
using EcommercePedidos.Objects.Dtos.Entities;
using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Interfaces;
using EcommercePedidos.Service.States;
using EcommercePedidos.Service.Strategies;

namespace EcommercePedidos.Service.Entities
{
    public class PedidoService : Pedido, IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _repository = pedidoRepository;
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

            IFrete frete = GerarFretePorTipo(entity.TipoFrete);

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

        public async Task<PedidoDTO> SucessoAoPagar(PedidoDTO entitiesDTO)
        {
            IEstadoPedido status = ObterStatusClasse(ConverterParaModel(entitiesDTO).StatusPedido);
            IEstadoPedido newStatus = status.SucessoAoPagar();
            entitiesDTO.StatusPedido = (int)ObterEstadoEnum(newStatus);

            await Atualizar(entitiesDTO, entitiesDTO.Id);

            return entitiesDTO;
        }

        public async Task<PedidoDTO> DespacharPedido(PedidoDTO entitiesDTO)
        {
            IEstadoPedido status = ObterStatusClasse(ConverterParaModel(entitiesDTO).StatusPedido);
            IEstadoPedido newStatus = status.DespacharPedido();
            entitiesDTO.StatusPedido = (int)ObterEstadoEnum(newStatus);

            await Atualizar(entitiesDTO, entitiesDTO.Id);

            return entitiesDTO;
        }

        public async Task<PedidoDTO> CancelarPedido(PedidoDTO entitiesDTO)
        {
            IEstadoPedido status = ObterStatusClasse(ConverterParaModel(entitiesDTO).StatusPedido);
            IEstadoPedido newStatus = status.CancelarPedido();
            entitiesDTO.StatusPedido = (int)ObterEstadoEnum(newStatus);

            await Atualizar(entitiesDTO, entitiesDTO.Id);

            return entitiesDTO;
        }
        private IFrete GerarFretePorTipo(TipoFrete tipoFrete)
        {
            return tipoFrete switch
            {
                TipoFrete.Aereo => new FreteAereo(),
                TipoFrete.Terrestre => new FreteTerrestre(),
                _ => throw new ArgumentException("Frete inválido"),
            };
        }

        private IEstadoPedido ObterStatusClasse(StatusPedido statusPedido)
        {
            return statusPedido switch
            {
                StatusPedido.AguardandoPagamento => new AguardandoPagamento(),
                StatusPedido.Pago => new Pago(),
                StatusPedido.Enviado => new Enviado(),
                StatusPedido.Cancelado => new Cancelado(),
                _ => throw new ArgumentException("Estado inválido"),
            }; 
        }

        private StatusPedido ObterEstadoEnum(IEstadoPedido state)
        {
            return state switch
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
                Produto = entitiesDTO.Produto,
                Valor = entitiesDTO.Valor,
                StatusPedido = (StatusPedido)entitiesDTO.StatusPedido,
                TipoFrete = (TipoFrete)entitiesDTO.TipoFrete,
            };

            return pedido;
        }
    }
}
