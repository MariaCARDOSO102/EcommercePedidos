using EcommercePedidos.Objects.Dtos.Entities;
using EcommercePedidos.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var pedidos = await _pedidoService.ListarTodos();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var pedido = await _pedidoService.ObterPorId(id);
            if (pedido == null)
                return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoDTO pedidoDTO)
        {
            await _pedidoService.GerarPedido(pedidoDTO);
            return CreatedAtAction(nameof(ObterPorId), new { id = pedidoDTO.Id }, pedidoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(int id, [FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                await _pedidoService.Atualizar(pedidoDTO, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("sucesso-pagamento")]
        public async Task<IActionResult> SucessoPagamento([FromBody] PedidoDTO pedidoDTO)
        {
            var atualizado = await _pedidoService.SucessoAoPagar(pedidoDTO);
            return Ok(atualizado);
        }

        [HttpPut("despachar")]
        public async Task<IActionResult> DespacharPedido([FromBody] PedidoDTO pedidoDTO)
        {
            var atualizado = await _pedidoService.DespacharPedido(pedidoDTO);
            return Ok(atualizado);
        }

        [HttpPut("cancelar")]
        public async Task<IActionResult> CancelarPedido([FromBody] PedidoDTO pedidoDTO)
        {
            var atualizado = await _pedidoService.CancelarPedido(pedidoDTO);
            return Ok(atualizado);
        }
    }
}
