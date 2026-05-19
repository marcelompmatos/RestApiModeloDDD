using RestApiModeloDDD.Domain.Core.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Domain.Interfaces.Repositories
{
    public interface IRepositoryPedido : IRepositoryBase<Pedido>
    {

        Task<List<Pedido>> GetPedidosCompletosAsync();


    }
}