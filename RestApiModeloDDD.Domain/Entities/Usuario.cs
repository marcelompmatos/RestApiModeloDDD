using System;

namespace RestApiModeloDDD.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string SenhaHash { get; set; }

        public string Role { get; set; }

        public bool Ativo { get; set; }
    }
}
