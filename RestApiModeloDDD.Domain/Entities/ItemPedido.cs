using RestApiModeloDDD.Domain.Entitys;

namespace RestApiModeloDDD.Domain.Entities
{
    public class ItemPedido : Base
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
