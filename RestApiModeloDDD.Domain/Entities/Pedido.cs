using RestApiModeloDDD.Domain.Entitys;
using System;
using System.Collections.Generic;

namespace RestApiModeloDDD.Domain.Entities
{
    public class Pedido : Base
    {
        public int ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ItemPedido> Itens { get; set; }
    }
}
