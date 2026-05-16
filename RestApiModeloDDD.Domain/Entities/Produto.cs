using RestApiModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace RestApiModeloDDD.Domain.Entitys
{
    public class Produto : Base
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool IsDisponivel { get; set; }

        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
