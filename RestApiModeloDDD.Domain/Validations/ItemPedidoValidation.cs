using FluentValidation;
using RestApiModeloDDD.Domain.Entities;

namespace RestApiModeloDDD.Domain.Validations
{
    public class ItemPedidoValidation : AbstractValidator<ItemPedido>
    {
        public ItemPedidoValidation()
        {
            RuleFor(x => x.ProdutoId)
                .GreaterThan(0)
                .WithMessage("Produto é obrigatório.");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .WithMessage("Quantidade deve ser maior que zero.");

            RuleFor(x => x.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("Valor unitário deve ser maior que zero.");
        }
    }
}
