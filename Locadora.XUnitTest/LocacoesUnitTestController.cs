using Locadora.Controllers;
using Locadora.Data.EF;
using Locadora.Data.EF.Repositories;
using Locadora.Domain.Contracts.Repositories;
using Locadora.Domain.Entities;
using Locadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Locadora.XUnitTest
{
    public class LocacoesUnitTestController
    {
        public static LocadoraDataContext locadoraDataContext { get; }

        private static ILocacoesRepository locacoesRepository;
        private static IClienteRepository clienteRepository;
        private static IFilmeRepository filmeRepository;

        static LocacoesUnitTestController()
        {
            DbContextOptions<LocadoraDataContext> options;
            var builder = new DbContextOptionsBuilder<LocadoraDataContext>();
            builder.UseInMemoryDatabase(databaseName: "Locadora");
            options = builder.Options;
            locadoraDataContext = new LocadoraDataContext(options);
            DbInitializer.Initializer(locadoraDataContext);

            locacoesRepository = new LocacoesRepositoryEF(locadoraDataContext);
            clienteRepository = new ClienteRepositoryEF(locadoraDataContext);
            filmeRepository = new FilmeRepositoryEF(locadoraDataContext);
        }


        // ---------------------------------------------------------------------------
        // Inicio dos Testes

        [Fact]
        public async Task GetAllLocacoes_Return_OkResult()
        {
            // Arrange
            var controller = new FilmesController(filmeRepository);

            // Act
            var data = await controller.GetAll();

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public async Task GetLocacoesById_Return_OkResult()
        {
            // Arrange
            var controller = new LocacoesController(locacoesRepository, clienteRepository, filmeRepository);

            // Act
            var data = await controller.GetById(1);

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }


        [Fact]
        public void PostLocacoes_Return_CreatedAtRouteResult()
        {
            // Arrange
            var controller = new LocacoesController(locacoesRepository, clienteRepository, filmeRepository);

            // Act
            var locacao = new LocacoesVM
            {
                IdCliente = 1,
                IdFilme = 9,
                DataLocacao = DateTime.Now,
                DevolucaoPrevista = DateTime.Now.AddDays(2)
            };

            var data = controller.Post(locacao);

            // Assert
            Assert.Equal(typeof(CreatedAtRouteResult), data.GetType());
        }

        [Fact]
        public async Task PutLocacoes_Return_NoContentResult()
        {
            // Arrange
            var controller = new LocacoesController(locacoesRepository, clienteRepository, filmeRepository);

            // Act
            var locacao = new LocacoesVM
            {
                IdCliente = 1,
                IdFilme = 2,
                DataLocacao = DateTime.Now,
                DevolucaoPrevista = DateTime.Now.AddDays(1)
            };
            var data = controller.Post(locacao) as CreatedAtRouteResult;
            var resultadoLocacao = data.Value as Locacoes;
            var devolucao = new LocacoesVM
            {
                Id = resultadoLocacao.Id,
                IdCliente = resultadoLocacao.IdCliente,
                IdFilme = resultadoLocacao.IdFilme,
                DataLocacao = resultadoLocacao.DataLocacao,
                DevolucaoPrevista = resultadoLocacao.DevolucaoPrevista,
                Devolucao = DateTime.Now.AddDays(2)
            };

            var dataDevolucao = await controller.Put(devolucao.Id, devolucao);

            // Assert
            Assert.Equal(typeof(NoContentResult), dataDevolucao.GetType());
        }


    }
}
