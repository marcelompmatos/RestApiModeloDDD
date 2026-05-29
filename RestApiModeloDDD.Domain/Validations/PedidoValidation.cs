using FluentValidation;
using RestApiModeloDDD.Domain.Entities;
using System;

namespace RestApiModeloDDD.Domain.Validations
{
    public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(p => p.ClienteId)
                .GreaterThan(0)
                .WithMessage("O cliente é obrigatório.");

            RuleFor(p => p.DataPedido)
                .NotEmpty()
                .WithMessage("A data do pedido é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do pedido não pode ser futura.");

            RuleFor(p => p.ValorTotal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor total não pode ser negativo.");

            RuleFor(p => p.Itens)
                .NotNull()
                .WithMessage("O pedido deve possuir itens.");

            RuleFor(p => p.Itens.Count)
                .GreaterThan(0)
                .WithMessage("O pedido deve possuir ao menos um item.");
        }
    }
}
