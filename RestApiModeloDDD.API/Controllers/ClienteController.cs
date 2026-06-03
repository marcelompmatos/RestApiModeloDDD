using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IApplicationServiceCliente _applicationServiceCliente;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(
            IApplicationServiceCliente applicationServiceCliente,
            ILogger<ClientesController> logger)
        {
            _applicationServiceCliente = applicationServiceCliente;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            var clientes =
                await _applicationServiceCliente.GetAllAsync();

            var lista = clientes.ToList();

            if (!lista.Any())
                return NoContent();

            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var cliente =
                await _applicationServiceCliente.GetByIdAsync(id);

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto clienteDto)
        {
            await _applicationServiceCliente.AddAsync(clienteDto);

            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromBody] ClienteDto clienteDto)
        {
            clienteDto.Id = id;

            await _applicationServiceCliente.UpdateAsync(clienteDto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicationServiceCliente.RemoveAsync(id);

            return NoContent();
        }
    }


}
