using RestApiModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RestApiModeloDDD.Application.Dtos
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }

        public ClienteDto Cliente { get; set; }

        public ICollection<ItemPedidoDTO> Itens { get; set; }
    }
}
