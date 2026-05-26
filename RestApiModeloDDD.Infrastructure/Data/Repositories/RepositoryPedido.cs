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

        public async Task<Pedido> GetPedidoAsync(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta de pedido no repositório. PedidoId: {PedidoId}",
                id);

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                _logger.LogWarning(
                    "Pedido não encontrado no repositório. PedidoId: {PedidoId}",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Pedido encontrado com sucesso no repositório. PedidoId: {PedidoId}",
                id);

            return pedido;
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            _logger.LogInformation(
                "Iniciando consulta de pedidos no repositório");

            var pedidos = await _context.Pedidos
                .Include(p => p.Cliente)
                .AsNoTracking()
                .ToListAsync();

            _logger.LogInformation(
                "Consulta de pedidos finalizada no repositório. Quantidade: {Quantidade}",
                pedidos.Count);

            return pedidos;
        }
    }
}
