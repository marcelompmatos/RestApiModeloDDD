using RestApiModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RestApiModeloDDD.Domain.Entitys
{
    public class Cliente : Base
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
