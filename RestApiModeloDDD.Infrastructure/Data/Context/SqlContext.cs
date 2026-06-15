using Microsoft.EntityFrameworkCore;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Entitys;
using System;
using System.Linq;

namespace RestApiModeloDDD.Infrastructure.Data.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Produto>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);

            builder.Entity<Pedido>()
                .Property(p => p.ValorTotal)
                .HasPrecision(18, 2);

            builder.Entity<ItemPedido>()
                .Property(p => p.ValorUnitario)
                .HasPrecision(18, 2);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}