using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IApplicationServiceProduto applicationServiceProduto;

        public ProdutosController(IApplicationServiceProduto applicationServiceProduto)
        {
            this.applicationServiceProduto = applicationServiceProduto;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
        {
            var produtos = await applicationServiceProduto.GetAllAsync();
            return Ok(produtos);
        }

        // GET api/values/5\
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> Get(int id)
        {
            var produto = await applicationServiceProduto.GetByIdAsync(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDto produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest();

            await applicationServiceProduto.AddAsync(produtoDTO);

            return Ok("Produto cadastrado com sucesso!");
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProdutoDto produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest();

            await applicationServiceProduto.UpdateAsync(produtoDTO);

            return Ok("Produto atualizado com sucesso!");
        }

        // DELETE api/values/5
        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] ProdutoDto produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest();

            await applicationServiceProduto.RemoveAsync(produtoDTO);

            return Ok("Produto removido com sucesso!");
        }
    }
}