using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmesController(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _filmeRepository.GetAsync();

            var filmes = data.Select(u => new FilmeVM { Id = u.Id, Titulo = u.Titulo, Ano = u.Ano, TituloOriginal = u.TituloOriginal, NoCatalogo = u.NoCatalogo });

            return Ok(filmes);
        }

        [HttpGet("{id:int}", Name = "GetFilmesById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _filmeRepository.GetAsync(id);

            if (data == null)
                return NotFound();

            var filmes = new FilmeVM { Id = data.Id, Titulo = data.Titulo, Ano = data.Ano, TituloOriginal = data.TituloOriginal, NoCatalogo = data.NoCatalogo };

            return Ok(filmes);
        }


        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var data = await _filmeRepository.GetFilmeTituloAsync(name);

            if (data == null)
                return NotFound();

            var filmes = data.Select(u => new FilmeVM { Id = u.Id, Titulo = u.Titulo, Ano = u.Ano, TituloOriginal = u.TituloOriginal, NoCatalogo = u.NoCatalogo });

            return Ok(filmes);
        }


        [HttpPost]
        public IActionResult Post([FromBody] FilmeVM model)
        {
            var filmeModel = _filmeRepository.GetFilmeTitulo(model.Titulo);

            if (filmeModel != null)
            {
                if (filmeModel.Count() > 0)
                {
                    ModelState.AddModelError("Filme", "Filme já cadastrado!");
                }
            }

            if (ModelState.IsValid)
            {
                var filme = new Filme()
                {
                    Titulo = model.Titulo,
                    TituloOriginal = model.TituloOriginal,
                    Ano = model.Ano
                };

                _filmeRepository.Add(filme);

                return CreatedAtRoute("GetFilmesById", new { filme.Id }, new { filme.Id, filme.Titulo, filme.TituloOriginal, filme.Ano, filme.NoCatalogo });
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, FilmeVM model)
        {
            if (ModelState.IsValid)
            {
                var filme = await _filmeRepository.GetAsync(id);

                if (filme == null)
                    return BadRequest("Filme não encontrado");

                filme.Titulo = model.Titulo;
                filme.TituloOriginal = model.TituloOriginal;
                filme.Ano = model.Ano;
                filme.NoCatalogo = model.NoCatalogo;

                _filmeRepository.Edit(filme);

                return NoContent();
            }
            return BadRequest(ModelState);
        }

    }
}
