using EcommercePedidos.Data.Interfaces;
using EcommercePedidos.Objects.Dtos.Entities;
using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using EcommercePedidos.Service.Interfaces;
using EcommercePedidos.Service.States;
using EcommercePedidos.Service.Strategies;

namespace EcommercePedidos.Service.Entities
{
    public class PedidoService : IPedidoService
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

        public async Task<PedidoDTO> GerarPedido(PedidoDTO entitiesDTO)
        {
            if (!Enum.IsDefined(typeof(StatusPedido), entitiesDTO.StatusPedido))
            {
                throw new ArgumentException("StatusPedido inválido.");
            }

            var entity = ConverterParaModel(entitiesDTO);

            IFrete frete = GerarFretePorTipo(entity.TipoFrete);
            entity.Valor = (float)frete.calcula(entity.SubTotal);

            await _repository.GerarPedido(entity);
            return ConverterParaDTO(entity);
        }

        public async Task<PedidoDTO> Atualizar(PedidoDTO entitiesDTO, int id)
        {
            var existingPedido = await _repository.GetById(id);

            if (existingPedido is null)
            {
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");
            }

            if (existingPedido.StatusPedido == StatusPedido.AguardandoPagamento)
            {
                IFrete frete = GerarFretePorTipo((TipoFrete)entitiesDTO.TipoFrete);
                entitiesDTO.Valor = (float)frete.calcula(entitiesDTO.SubTotal);
            }
            else
            {
                throw new Exception("Não é permitido atualizar o pedido, após seu cancelamento ou despache.");
            }

            var pedido = ConverterParaModel(entitiesDTO);
            await _repository.Update(pedido);

            return entitiesDTO;
        }   

        public async Task<PedidoDTO> SucessoAoPagar(PedidoDTO entitiesDTO)
        {
            var pedido = ConverterParaModel(entitiesDTO);

            IEstadoPedido status = ObterStatusClasse(ConverterParaModel(entitiesDTO).StatusPedido);
            IEstadoPedido newStatus = status.SucessoAoPagar();
            entitiesDTO.StatusPedido = (int)ObterEstadoEnum(newStatus);

            await _repository.Update(pedido); 

            return entitiesDTO;
        }

        public async Task<PedidoDTO> DespacharPedido(PedidoDTO entitiesDTO)
        {
            var pedido = ConverterParaModel(entitiesDTO);

            IEstadoPedido status = ObterStatusClasse(ConverterParaModel(entitiesDTO).StatusPedido);
            IEstadoPedido newStatus = status.DespacharPedido();
            entitiesDTO.StatusPedido = (int)ObterEstadoEnum(newStatus);

            await _repository.Update(pedido);

            return entitiesDTO;
        }

        public async Task<PedidoDTO> CancelarPedido(PedidoDTO entitiesDTO)
        {
            var pedido = ConverterParaModel(entitiesDTO);

            IEstadoPedido status = ObterStatusClasse(ConverterParaModel(entitiesDTO).StatusPedido);
            IEstadoPedido newStatus = status.CancelarPedido();
            entitiesDTO.StatusPedido = (int)ObterEstadoEnum(newStatus);

            await _repository.Update(pedido);

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
                Cancelado _ => StatusPedido.Cancelado,
                Enviado _ => StatusPedido.Enviado,
                Pago _ => StatusPedido.Pago,
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
                SubTotal = pedido.SubTotal,
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
                SubTotal = (float)entitiesDTO.SubTotal,
                StatusPedido = (StatusPedido)entitiesDTO.StatusPedido,
                TipoFrete = (TipoFrete)entitiesDTO.TipoFrete,
            };

            return pedido;
        }
    }
}
