using Microsoft.AspNetCore.Mvc;
using RestApiModeloDDD.Application.Dtos;
using RestApiModeloDDD.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiModeloDDD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IApplicationServiceCliente applicationServiceCliente;


        public ClientesController(IApplicationServiceCliente applicationServiceCliente)
        {
            this.applicationServiceCliente = applicationServiceCliente;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            var clientes = await applicationServiceCliente.GetAllAsync();
            return Ok(clientes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var cliente = await applicationServiceCliente.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteDto clienteDTO)
        {
            if (clienteDTO == null)
                return BadRequest();

            await applicationServiceCliente.AddAsync(clienteDTO);

            return Ok("Cliente cadastrado com sucesso!");
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ClienteDto clienteDTO)
        {
            if (clienteDTO == null)
                return BadRequest();

            await applicationServiceCliente.UpdateAsync(clienteDTO);

            return Ok("Cliente atualizado com sucesso!");
        }

        // DELETE api/values/5
        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody] ClienteDto clienteDTO)
        {
            if (clienteDTO == null)
                return BadRequest();

            await applicationServiceCliente.RemoveAsync(clienteDTO);

            return Ok("Cliente removido com sucesso!");
        }
    }
}