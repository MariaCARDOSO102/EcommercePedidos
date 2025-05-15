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
        public async Task<IActionResult> ListAll()
        {
            var pedidos = await _pedidoService.ListAll();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetById(id);
                return Ok(pedido);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoDTO pedidoDTO)
        {
            await _pedidoService.GerarPedido(pedidoDTO);
            return CreatedAtAction(nameof(GetById), new { id = pedidoDTO.Id }, pedidoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(int id, [FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                var pedidoAtualizado = await _pedidoService.Atualizar(pedidoDTO, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}/sucesso-pagamento")]
        public async Task<IActionResult> SucessoPagamento(int id)
        {
            try
            {
                var atualizado = await _pedidoService.SucessoAoPagar(id);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/despachar")]
        public async Task<IActionResult> DespacharPedido(int id)
        {
            try
            {
                var atualizado = await _pedidoService.DespacharPedido(id);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            try
            {
                var atualizado = await _pedidoService.CancelarPedido(id);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
