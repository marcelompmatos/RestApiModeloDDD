using AutoMapper;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        private readonly IServiceProduto serviceProduto;
        private readonly IMapper mapper;

        public ApplicationServiceProduto(IServiceProduto serviceProduto
                                        , IMapper mapper)
        {
            this.serviceProduto = serviceProduto;
            this.mapper = mapper;
        }


        public async Task AddAsync(ProdutoDto produtoDto)
        {
            var produto = mapper.Map<Produto>(produtoDto);

            await serviceProduto.AddAsync(produto);
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
        {
            var produtos = await serviceProduto.GetAllAsync();

            return mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            var produto = await serviceProduto.GetByIdAsync(id);

            return mapper.Map<ProdutoDto>(produto);
        }

        public async Task RemoveAsync(ProdutoDto produtoDto)
        {
            var produto = mapper.Map<Produto>(produtoDto);

            await serviceProduto.RemoveAsync(produto);
        }

        public async Task UpdateAsync(ProdutoDto produtoDto)
        {
            var produto = mapper.Map<Produto>(produtoDto);

            await serviceProduto.UpdateAsync(produto);
        }


    }
}