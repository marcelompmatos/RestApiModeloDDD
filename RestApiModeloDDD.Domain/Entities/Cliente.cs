using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Validations;
using System.Collections.Generic;
using FluentValidation.Results;

namespace RestApiModeloDDD.Domain.Entitys
{
    public class Cliente : Base
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Cliente() { }

        public Cliente(string nome, string sobrenome, string email, ICollection<Pedido> pedidos)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Pedidos = pedidos;
        }
        public ValidationResult Validate()
            => new ClienteValidation().Validate(this);
    }
    
 }
