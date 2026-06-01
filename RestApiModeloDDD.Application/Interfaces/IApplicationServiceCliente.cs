using RestApiModeloDDD.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Interfaces
{
    public interface IApplicationServiceCliente
    {
        Task AddAsync(ClienteDto clienteDto);

        Task UpdateAsync(ClienteDto clienteDto);

        Task RemoveAsync(int id);

        Task<IEnumerable<ClienteDto>> GetAllAsync();

        Task<ClienteDto> GetByIdAsync(int id);
    }
}