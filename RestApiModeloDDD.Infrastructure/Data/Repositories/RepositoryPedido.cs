using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryPedido : RepositoryBase<Pedido>, IRepositoryPedido
    {
        private readonly SqlContext _context;
        private readonly ILogger<RepositoryPedido> _logger;

        public RepositoryPedido(
            SqlContext sqlContext,
            ILogger<RepositoryPedido> logger)
            : base(sqlContext)
        {
            _context = sqlContext;
            _logger = logger;
        }

        public async Task<Pedido?> GetPedidoAsync(int id)
        {
            _logger.LogInformation(
                "Consultando pedido. PedidoId: {PedidoId}",
                id);

            var pedido = await _context.Pedidos
                .AsNoTracking()
                .AsSplitQuery()
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido is null)
            {
                _logger.LogWarning(
                    "Pedido não encontrado. PedidoId: {PedidoId}",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Pedido encontrado. PedidoId: {PedidoId}",
                id);

            return pedido;
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            _logger.LogInformation("Consultando pedidos no repositório");

            var pedidos = await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .ToListAsync();

            _logger.LogInformation(
                "Consulta finalizada. Quantidade: {Quantidade}",
                pedidos.Count);

            return pedidos;
        }
    }
}
