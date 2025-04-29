using EcommercePedidos.Service.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePedidos.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ControllerPedido : Controller
    {
        private readonly PedidoService _pedidoService;

    }
}
