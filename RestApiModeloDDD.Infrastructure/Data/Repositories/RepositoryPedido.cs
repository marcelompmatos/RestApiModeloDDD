using Microsoft.EntityFrameworkCore;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Infrastructure.Data.Repositories
{
    public class RepositoryPedido : RepositoryBase<Pedido>, IRepositoryPedido
    {
        private readonly SqlContext _context;

        public RepositoryPedido(SqlContext sqlContext)
            : base(sqlContext)
        {
            this._context = sqlContext;
        }

        public async Task<Pedido> GetPedidoAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {

            return await _context.Pedidos
                        .Include(p => p.Cliente)
                        .AsNoTracking()
                        .ToListAsync();


        }
    }
}
