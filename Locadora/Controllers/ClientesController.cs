using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Locadora.Domain.Contracts.Repositories;
using Locadora.Models;
using Locadora.Domain.Entities;

namespace Locadora.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _clienteRepository.GetAsync();

            var clientes = data.Select(u => new ClienteVM { Id = u.Id, Nome = u.Nome, CPF = u.CPF });

            return Ok(clientes);
        }

        [HttpGet("{id:int}", Name = "GetClientesById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _clienteRepository.GetAsync(id);

            if (data == null)
                return NotFound();

            var clientes = new ClienteVM() { Id = data.Id, Nome = data.Nome, CPF = data.CPF };

            return Ok(clientes);
        }


        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var data = await _clienteRepository.GetClienteNomeAsync(name);

            if (data == null)
                return NotFound();

            var clientes = data.Select(u => new ClienteVM { Id = u.Id, Nome = u.Nome, CPF = u.CPF });

            return Ok(clientes);
        }


        [HttpPost]
        public IActionResult Post([FromBody] ClienteVM model)
        {
            var cliente =_clienteRepository.GetClienteCPF(model.CPF);

            if (cliente != null)
            {
                if (cliente.Count() > 0)
                {
                    ModelState.AddModelError("CPF", "CPF já cadastrado!");
                }
            }

            if (ModelState.IsValid)
            {
                var cli = new Cliente()
                {
                    Nome = model.Nome,
                    CPF = model.CPF
                };

                _clienteRepository.Add(cli);

                return CreatedAtRoute("GetClientesById", new { cli.Id }, new { cli.Id, cli.Nome, cli.CPF });
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ClienteVM model)
        {
            if (ModelState.IsValid)
            {
                var cliente = await _clienteRepository.GetAsync(id);

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

                cliente.Nome = model.Nome;
                cliente.CPF = model.CPF;

                _clienteRepository.Edit(cliente);

                return NoContent();
            }
            return BadRequest(ModelState);
        }

    }
}
