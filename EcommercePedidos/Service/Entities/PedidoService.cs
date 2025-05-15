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

        public async Task<IEnumerable<PedidoDTO>> ListAll()
        {
            var pedidos = await _repository.Get();
            List<PedidoDTO> entitiesDTO = [];

            foreach (var entity in pedidos)
            {
                entitiesDTO.Add(ConverterParaDTO(entity));
            }

            return entitiesDTO;
        }

        public async Task<PedidoDTO> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");

            return ConverterParaDTO(entity);
        }

        public async Task<PedidoDTO> GerarPedido(PedidoDTO entitiesDTO)
        {
            if (!Enum.IsDefined(typeof(StatusPedido), entitiesDTO.StatusPedido))
            {
                throw new ArgumentException("StatusPedido inválido.");
            }

            entitiesDTO.StatusPedido = (int)StatusPedido.AguardandoPagamento;

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
                throw new Exception("Não é permitido atualizar o pedido, após seu pagamento, cancelamento ou despache.");
            }

            var pedido = ConverterParaModel(entitiesDTO);
            await _repository.Update(pedido);

            return entitiesDTO;
        }   

        public async Task<PedidoDTO> SucessoAoPagar(int id)
        {
            var pedido = await _repository.GetById(id);

            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");

            IEstadoPedido estadoAtual = ObterStatusClasse(pedido.StatusPedido);
            IEstadoPedido novoEstado = estadoAtual.SucessoAoPagar();
            pedido.StatusPedido = ObterEstadoEnum(novoEstado);

            await _repository.Update(pedido);

            return ConverterParaDTO(pedido);
        }

        public async Task<PedidoDTO> DespacharPedido(int id)
        {
            var pedido = await _repository.GetById(id);

            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");

            IEstadoPedido estadoAtual = ObterStatusClasse(pedido.StatusPedido);
            IEstadoPedido novoEstado = estadoAtual.DespacharPedido();
            pedido.StatusPedido = ObterEstadoEnum(novoEstado);

            await _repository.Update(pedido);

            return ConverterParaDTO(pedido);
        }

        public async Task<PedidoDTO> CancelarPedido(int id)
        {
            var pedido = await _repository.GetById(id);

            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");

            IEstadoPedido estadoAtual = ObterStatusClasse(pedido.StatusPedido);
            IEstadoPedido novoEstado = estadoAtual.CancelarPedido();
            pedido.StatusPedido = ObterEstadoEnum(novoEstado);

            await _repository.Update(pedido);

            return ConverterParaDTO(pedido);
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
