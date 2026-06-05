using FluentValidation;
using RestApiModeloDDD.Domain.Exceptions;
using System.Linq;

namespace RestApiModeloDDD.Domain.Helpers
{
    public static class ValidationHelper
    {
        public static void Validate<TEntity>(TEntity entity,AbstractValidator<TEntity> validator)
        {
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                throw new DomainValidationException(
                    result.Errors.Select(x => x.ErrorMessage));
            }
        }
    }
}
