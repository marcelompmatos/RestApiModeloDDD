using FluentValidation;
using RestApiModeloDDD.Domain.Entitys;
namespace RestApiModeloDDD.Domain.Validations
{
    public class ClienteValidation:AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(c => c.Sobrenome)
                .NotEmpty()
                .WithMessage("O sobrenome é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O sobrenome deve ter no máximo 100 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("E-mail inválido.")
                .MaximumLength(150)
                .WithMessage("O e-mail deve ter no máximo 150 caracteres.");
        }
    }
}
