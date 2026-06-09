using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProdutosController : ControllerBase
{
    private readonly IApplicationServiceProduto _applicationServiceProduto;

    public ProdutosController(
        IApplicationServiceProduto applicationServiceProduto)
    {
        _applicationServiceProduto = applicationServiceProduto;
    }

    /// <summary>
    /// Retorna todos os produtos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var produtos =
            await _applicationServiceProduto.GetAllAsync();

        if (!produtos.Any())
            return NoContent();

        return Ok(produtos);
    }

    /// <summary>
    /// Retorna um produto pelo Id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProdutoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProdutoDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var produto =
            await _applicationServiceProduto.GetByIdAsync(id);

        if (produto is null)
            return NotFound();

        return Ok(produto);
    }

    /// <summary>
    /// Cadastra um novo produto.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] ProdutoDto produtoDto,
        CancellationToken cancellationToken)
    {
        var produtoId =
            await _applicationServiceProduto.AddAsync(produtoDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = produtoId },
            new
            {
                Id = produtoId,
                Message = "Produto criado com sucesso."
            });
    }

    /// <summary>
    /// Atualiza um produto existente.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] ProdutoDto produtoDto,
        CancellationToken cancellationToken)
    {
        produtoDto.Id = id;

        await _applicationServiceProduto.UpdateAsync(produtoDto);

        return NoContent();
    }

    /// <summary>
    /// Remove um produto.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id,
        CancellationToken cancellationToken)
    {
        await _applicationServiceProduto.RemoveAsync(id);

        return NoContent();
    }
}