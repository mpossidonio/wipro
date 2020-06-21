using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Locadora.Domain.Contracts.Repositories;
using Locadora.Domain.Entities;
using Locadora.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocacoesController : ControllerBase
    {
        private readonly ILocacoesRepository _locacoesRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFilmeRepository _filmeRepository;

        public LocacoesController(ILocacoesRepository locacoesRepository, IClienteRepository clienteRepository, IFilmeRepository filmeRepository)
        {
            _locacoesRepository = locacoesRepository;
            _clienteRepository = clienteRepository;
            _filmeRepository = filmeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _locacoesRepository.GetAsync();

            var cliente = await _clienteRepository.GetAsync();
            var filme = await _filmeRepository.GetAsync();

            var locacoes = from l in data
                           join c in cliente on l.IdCliente equals c.Id
                           join f in filme on l.IdFilme equals f.Id
                           select new LocacoesVM
                           {
                               Id = l.Id,
                               IdCliente = l.IdCliente,
                               IdFilme = l.IdFilme,
                               DataLocacao = l.DataLocacao,
                               DevolucaoPrevista = l.DevolucaoPrevista,
                               Devolucao = l.Devolucao,
                               Nome = c.Nome,
                               Titulo = f.Titulo,
                               TituloOriginal = f.TituloOriginal,
                               Alerta = (DateTime.Compare(Convert.ToDateTime(l.Devolucao), l.DevolucaoPrevista) > 0) ? "ATRASO NA DEVOLUÇÃO!!!" : ""
                           };

            return Ok(locacoes);
        }

        [HttpGet("{id:int}", Name = "GetLocacoesById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _locacoesRepository.GetAsync(id);

            if (data == null)
                return NotFound();

            var cliente = await _clienteRepository.GetAsync(data.IdCliente);
            var filme = await _filmeRepository.GetAsync(data.IdFilme);

            var locacoes = new LocacoesVM
            {
                Id = data.Id,
                IdCliente = data.IdCliente,
                IdFilme = data.IdFilme,
                DataLocacao = data.DataLocacao,
                DevolucaoPrevista = data.DevolucaoPrevista,
                Devolucao = data.Devolucao,
                Nome = cliente.Nome,
                Titulo = filme.Titulo,
                TituloOriginal = filme.TituloOriginal,
                Alerta = (DateTime.Compare((data.Devolucao==null?data.DevolucaoPrevista: Convert.ToDateTime(data.Devolucao)), data.DevolucaoPrevista)>0) ? "ATRASO NA DEVOLUÇÃO!!!" : ""
            };

            return Ok(locacoes);
        }


        [HttpPost]
        public IActionResult Post([FromBody] LocacoesVM model)
        {
            var locacao = _locacoesRepository.GetLocacao(model.IdCliente, model.IdFilme, model.DataLocacao.ToString());

            if (locacao != null)
            {
                if (locacao.Count() > 0)
                {
                    ModelState.AddModelError("Locacao", "Locação já efetuada!");
                }
            }

            var filme = _filmeRepository.Get(model.IdFilme);
            if (filme == null)
            {
                ModelState.AddModelError("Locacao", "Filme não encontrado!");
            }
            else
            {
                if (model.Devolucao == null)
                {
                    if (filme.NoCatalogo == false)
                    {
                        ModelState.AddModelError("Locacao", "Filme indiponivel para locacao!");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var loc = new Locacoes()
                {
                    IdCliente = model.IdCliente,
                    IdFilme = model.IdFilme,
                    DataLocacao = model.DataLocacao,
                    DevolucaoPrevista = model.DevolucaoPrevista,
                    Devolucao = model.Devolucao
                };

                _locacoesRepository.Add(loc);

                filme.NoCatalogo = (model.Devolucao == null) ? false : true;
                _filmeRepository.Edit(filme);

                return CreatedAtRoute("GetLocacoesById", new { loc.Id }, loc);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, LocacoesVM model)
        {
            if (ModelState.IsValid)
            {
                var locacoes = await _locacoesRepository.GetAsync(id);

                if (locacoes == null)
                    return BadRequest("Locacao não encontrado");

                locacoes.Id = id;
                locacoes.IdCliente = model.IdCliente;
                locacoes.IdFilme = model.IdFilme;
                locacoes.DataLocacao = model.DataLocacao;
                locacoes.DevolucaoPrevista = model.DevolucaoPrevista;
                locacoes.Devolucao = model.Devolucao;

                _locacoesRepository.Edit(locacoes);

                return NoContent();
            }
            return BadRequest(ModelState);
        }

    }
}
