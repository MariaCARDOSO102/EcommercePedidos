using EcommercePedidos.Data.Interfaces;
using EcommercePedidos.Objects.Models;
using System.Threading;

namespace EcommercePedidos.Data.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
