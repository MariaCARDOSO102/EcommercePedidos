using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Objects.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Threading;

namespace EcommercePedidos.Data.Builders
{
    public class PedidoBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configura a chave primária
            modelBuilder.Entity<Pedido>().HasKey(pg => pg.Id);
            modelBuilder.Entity<Pedido>().Property(pg => pg.Produto)
                .IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Pedido>().Property(pg => pg.Valor)
                .IsRequired().HasMaxLength(50); 

            // Inserção de dados iniciais (opcional)
            modelBuilder.Entity<Pedido>()
                .HasData(new List<Pedido>
                {
                new Pedido(1, "Blusa", (float)20.5, StatusPedido.Pago, TipoFrete.Terrestre),
                new Pedido(2, "Calça", (float)50.50, StatusPedido.Pago, TipoFrete.Terrestre),
                new Pedido(3, "Sapato", (float)100.00, StatusPedido.Pago, TipoFrete.Terrestre),
                });
        }
    }
}
