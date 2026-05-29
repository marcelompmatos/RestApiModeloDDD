using FluentValidation;
using RestApiModeloDDD.Domain.Entitys;

namespace RestApiModeloDDD.Domain.Validations
{
    public class ProdutoValidation:AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(150)
                .WithMessage("O nome do produto deve ter no máximo 150 caracteres.");

            RuleFor(p => p.Valor)
                .GreaterThan(0)
                .WithMessage("O valor do produto deve ser maior que zero.");

            RuleFor(p => p.IsDisponivel)
                .NotNull()
                .WithMessage("O status de disponibilidade deve ser informado.");
        }
    }
}
