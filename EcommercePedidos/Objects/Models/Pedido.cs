using EcommercePedidos.Objects.Enums;
using EcommercePedidos.Service.States;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Xml.Linq;

namespace EcommercePedidos.Objects.Models
{
    [Table("pedido")]
    public class Pedido
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("produto")]
        public string Produto { get; set; }

        [Column("valor")]
        public float Valor { get; set; }

        [Column("statuspedido")]
        public StatusPedido StatusPedido { get; set; }

        [Column("tipofrete")]
        public TipoFrete TipoFrete { get; set; }

        public Pedido() 
        {
            StatusPedido = StatusPedido.AguardandoPagamento;
        }
        public Pedido(int id, string produto, float valor, StatusPedido statusPedido, TipoFrete tipoFrete)
        {
            Id = id;
            Produto = produto;
            Valor = valor;
            StatusPedido = StatusPedido.AguardandoPagamento;
            TipoFrete = tipoFrete;
        }     
    }
}
