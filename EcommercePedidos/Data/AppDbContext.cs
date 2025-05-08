using EcommercePedidos.Data.Builders;
using EcommercePedidos.Objects.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace EcommercePedidos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            PedidoBuilder.Build(modelBuilder);

        }
    }
}
