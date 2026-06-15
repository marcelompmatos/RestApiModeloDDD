using System;

namespace RestApiModeloDDD.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        public string TokenHash { get; set; }

        public DateTime ExpiraEm { get; set; }

        public bool Revogado { get; set; }

        public DateTime CriadoEm { get; set; }

        public Usuario Usuario { get; set; }
    }
}
