using FluentValidation.Results;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Validations;
using System.Collections.Generic;

namespace RestApiModeloDDD.Domain.Entitys
{
    public class Produto : Base
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool IsDisponivel { get; set; }

        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

        public Produto() { }

        public Produto(string nome, decimal valor, bool isDisponivel, ICollection<ItemPedido> itensPedido)
        {
            Nome = nome;
            Valor = valor;
            IsDisponivel = isDisponivel;
            ItensPedido = itensPedido;
        }
        public ValidationResult Validate()
         => new ProdutoValidation().Validate(this);
    }
}
