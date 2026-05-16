using RestApiModeloDDD.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        Task AddAsync(ProdutoDto produtoDto);

        Task UpdateAsync(ProdutoDto produtoDto);

        Task RemoveAsync(ProdutoDto produtoDto);

        Task<IEnumerable<ProdutoDto>> GetAllAsync();

        Task<ProdutoDto> GetByIdAsync(int id);
    }
}