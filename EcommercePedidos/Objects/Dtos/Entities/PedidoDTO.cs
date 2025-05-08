namespace EcommercePedidos.Objects.Dtos.Entities
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public float Valor { get; set; }
        public int StatusPedido { get; set; }
        public int TipoFrete { get; set; }
    }
}
