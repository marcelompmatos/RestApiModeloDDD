using RestApiModeloDDD.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        Task<int> AddAsync(ProdutoDto produtoDto);

        Task UpdateAsync(ProdutoDto produtoDto);

        Task RemoveAsync(int id);
        Task<IEnumerable<ProdutoDto>> GetAllAsync();

        Task<ProdutoDto> GetByIdAsync(int id);
    }
}