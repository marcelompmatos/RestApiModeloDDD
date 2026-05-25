using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IApplicationServiceProduto applicationServiceProduto;
        private readonly ILogger<PedidoController> _logger;

        public ProdutosController(IApplicationServiceProduto applicationServiceProduto,  ILogger<PedidoController> logger)
        {
            this.applicationServiceProduto = applicationServiceProduto;
            this._logger = logger;
        }

        // GET api/produto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
        {
            _logger.LogInformation(
                "Iniciando consulta de produtos");

            var produtos = await applicationServiceProduto
                .GetAllAsync();

            var listaProdutos = produtos.ToList();

            _logger.LogInformation(
                "Consulta de produtos finalizada. Quantidade: {Quantidade}",
                listaProdutos.Count);

            return Ok(listaProdutos);
        }

        // GET api/produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> Get(int id)
        {
            _logger.LogInformation(
                "Iniciando consulta do produto. Id: {ProdutoId}",
                id);

            var produto = await applicationServiceProduto
                .GetByIdAsync(id);

            if (produto == null)
            {
                _logger.LogWarning(
                    "Produto não encontrado. Id: {ProdutoId}",
                    id);

                return NotFound();
            }

            _logger.LogInformation(
                "Produto encontrado com sucesso. Id: {ProdutoId}",
                id);

            return Ok(produto);
        }

        // POST api/produto
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDto produtoDTO)
        {
            _logger.LogInformation(
                "Iniciando cadastro do produto. Nome: {NomeProduto}",
                produtoDTO?.Nome);

            if (produtoDTO == null)
            {
                _logger.LogWarning(
                    "Tentativa de cadastro de produto com payload nulo");

                return BadRequest();
            }

            await applicationServiceProduto
                .AddAsync(produtoDTO);

            _logger.LogInformation(
                "Produto cadastrado com sucesso. Nome: {NomeProduto}",
                produtoDTO.Nome);

            return Ok(new
            {
                mensagem = "Produto cadastrado com sucesso!"
            });
        }

        // PUT api/produto
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProdutoDto produtoDTO)
        {
            _logger.LogInformation(
                "Iniciando atualização do produto. Id: {ProdutoId}",
                produtoDTO?.Id);

            if (produtoDTO == null)
            {
                _logger.LogWarning(
                    "Tentativa de atualização de produto com payload nulo");

                return BadRequest();
            }

            await applicationServiceProduto
                .UpdateAsync(produtoDTO);

            _logger.LogInformation(
                "Produto atualizado com sucesso. Id: {ProdutoId}",
                produtoDTO.Id);

            return Ok(new
            {
                mensagem = "Produto atualizado com sucesso!"
            });
        }

        // DELETE api/produto
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] ProdutoDto produtoDTO)
        {
            _logger.LogInformation(
                "Iniciando remoção do produto. Id: {ProdutoId}",
                produtoDTO?.Id);

            if (produtoDTO == null)
            {
                _logger.LogWarning(
                    "Tentativa de remoção de produto com payload nulo");

                return BadRequest();
            }

            await applicationServiceProduto
                .RemoveAsync(produtoDTO);

            _logger.LogInformation(
                "Produto removido com sucesso. Id: {ProdutoId}",
                produtoDTO.Id);

            return Ok(new
            {
                mensagem = "Produto removido com sucesso!"
            });
        }
    }
}