using System;

namespace RestApiModeloDDD.Domain.Entities
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool IsAtivo { get; set; } = true;
    }
}
