namespace RestApiModeloDDD.Application.Dtos
{
    public class ItemPedidoDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public ProdutoDto Produto { get; set; }
    }
}
