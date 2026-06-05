using AutoMapper;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using RestApiModeloDDD.Domain.Core.Interfaces.Services;
using RestApiModeloDDD.Domain.Entitys;
using RestApiModeloDDD.Domain.Exceptions;
using RestApiModeloDDD.Domain.Helpers;
using RestApiModeloDDD.Domain.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.Application.Services
{
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        private readonly IServiceProduto serviceProduto;
        private readonly IMapper mapper;
        private readonly ILogger<ApplicationServicePedido> _logger;


        public ApplicationServiceProduto(IServiceProduto serviceProduto
                                        , IMapper mapper, ILogger<ApplicationServicePedido> logger)
        {
            this.serviceProduto = serviceProduto;
            this.mapper = mapper;
            this._logger = logger;
        }


        public async Task AddAsync(ProdutoDto produtoDto)
        {
            _logger.LogInformation(
                "Iniciando cadastro de produto na camada Application. Nome: {NomeProduto}",
                produtoDto?.Nome);

            var produto = mapper.Map<Produto>(produtoDto);

            var result = produto.Validate();

            if (!result.IsValid)
            {
                var erros = result.Errors
                    .Select(x => x.ErrorMessage);

                _logger.LogWarning(
                    "Erro de validação da entidade Cliente. Erros: {Erros}",
                    string.Join(" | ", erros));

                throw new DomainValidationException(erros);
            }

            await serviceProduto.AddAsync(produto);

            _logger.LogInformation(
                "Produto cadastrado com sucesso na camada Application. Nome: {NomeProduto}",
                produtoDto?.Nome);
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
        {
            _logger.LogInformation(
                "Iniciando consulta de produtos na camada Application");

            var produtos = await serviceProduto.GetAllAsync();

            var listaProdutos = produtos.ToList();

            _logger.LogInformation(
                "Consulta de produtos finalizada na camada Application. Quantidade: {Quantidade}",
                listaProdutos.Count);

            return mapper.Map<IEnumerable<ProdutoDto>>(listaProdutos);
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta de produto por Id na camada Application. Id: {ProdutoId}",
                id);

            var produto = await serviceProduto.GetByIdAsync(id);

            if (produto == null)
            {
                _logger.LogWarning(
                    "Produto não encontrado na camada Application. Id: {ProdutoId}",
                    id);

                return null;
            }

            _logger.LogInformation(
                "Produto encontrado com sucesso na camada Application. Id: {ProdutoId}",
                id);

            return mapper.Map<ProdutoDto>(produto);
        }

        public async Task RemoveAsync(ProdutoDto produtoDto)
        {
            _logger.LogInformation(
                "Iniciando remoção de produto na camada Application. Id: {ProdutoId}",
                produtoDto?.Id);


            try
            {
                var produto = mapper.Map<Produto>(produtoDto);
                await serviceProduto.RemoveAsync(produto.Id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao remover produto na camada Application. Id: {ProdutoId}",
                    produtoDto?.Id);
                throw;
            }
      

            

            _logger.LogInformation(
                "Produto removido com sucesso na camada Application. Id: {ProdutoId}",
                produtoDto?.Id);
        }

        public async Task UpdateAsync(ProdutoDto produtoDto)
        {
            _logger.LogInformation(
                "Iniciando atualização de produto na camada Application. Id: {ProdutoId}",
                produtoDto?.Id);

            var produto = mapper.Map<Produto>(produtoDto);

            await serviceProduto.UpdateAsync(produto);

            _logger.LogInformation(
                "Produto atualizado com sucesso na camada Application. Id: {ProdutoId}",
                produtoDto?.Id);
        }


    }
}