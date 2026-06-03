using System.Collections.Generic;
using System.Linq;

namespace RestApiModeloDDD.Domain.Exceptions
{
    public sealed class DomainValidationException : DomainException
    {
        public IReadOnlyCollection<string> Errors { get; }

        public DomainValidationException(string error) : base(error)
        {
            Errors = new[] { error };
        }

        public DomainValidationException(IEnumerable<string> errors) : base("Erro de validação.")
        {
            Errors = errors.ToList();
        }
    }
}