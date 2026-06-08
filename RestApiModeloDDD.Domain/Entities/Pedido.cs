using FluentValidation.Results;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Domain.Validations;
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

        public Pedido() { }

        public Pedido(int clienteId, DateTime dataPedido, decimal valorTotal, Cliente cliente, ICollection<ItemPedido> itens)
        {
            ClienteId = clienteId;
            DataPedido = dataPedido;
            ValorTotal = valorTotal;
            Cliente = cliente;
            Itens = itens;
        }

        public ValidationResult Validate()
            => new PedidoValidation().Validate(this);
    }
}

