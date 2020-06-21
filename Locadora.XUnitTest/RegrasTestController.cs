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
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Locadora.XUnitTest
{
    public class RegrasTestController
    {
        public static LocadoraDataContext locadoraDataContext { get; }

        private static IClienteRepository clienteRepository;
        private static IFilmeRepository filmeRepository;
        private static ILocacoesRepository locacoesRepository;

        static RegrasTestController()
        {
            DbContextOptions<LocadoraDataContext> options;
            var builder = new DbContextOptionsBuilder<LocadoraDataContext>();
            builder.UseInMemoryDatabase(databaseName: "Locadora");
            options = builder.Options;
            locadoraDataContext = new LocadoraDataContext(options);
            DbInitializer.Initializer(locadoraDataContext);

            clienteRepository = new ClienteRepositoryEF(locadoraDataContext);
            filmeRepository = new FilmeRepositoryEF(locadoraDataContext);
            locacoesRepository = new LocacoesRepositoryEF(locadoraDataContext);
        }


        // ---------------------------------------------------------------------------
        // Inicio dos Testes

        [Fact]
        public void UmLocadorNaoPodeSeRepetir() 
        {
            // Arrange
            var controller = new ClientesController(clienteRepository);

            // Act
            // --> Tenta inserir um CPF já existente
            var cliente = new ClienteVM
            {
                Nome = "Teste Duplicidade",
                CPF = "11111111111"
            };

            var data = controller.Post(cliente);
            
            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), data.GetType());
        }

        [Fact(Skip="Não há métodos DELETE para serem testados")]
        public void NaoEPermitidoExcluirFisicamenteUmRegistro()
        {
            // Não há métodos DELETE para serem testados

            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public void NaoPermitirAlugarUmFilmeQueNaoEstaDisponivel()
        {
            // Arrange
            var controller = new LocacoesController(locacoesRepository, clienteRepository, filmeRepository);

            // Act
            // --> 1a locacao
            var locacao = new LocacoesVM
            {
                IdCliente = 1,
                IdFilme = 9,
                DataLocacao = DateTime.Now,
                DevolucaoPrevista = DateTime.Now.AddDays(2)
            };
            var data = controller.Post(locacao);
            
            // --> 2a locacao mesmo filme
            locacao.IdCliente = 2;
            data = controller.Post(locacao);
            
            // Assert
            Assert.Equal(typeof(BadRequestObjectResult), data.GetType());
        }

        [Fact]
        public async Task AlertarNaDevoluçãoSeOFilmeEstaComAtraso()
        {
            // Arrange
            var controller = new LocacoesController(locacoesRepository, clienteRepository, filmeRepository);

            // Act
            // --> Feita nova locacao
            var locacao = new LocacoesVM
            {
                IdCliente = 1,
                IdFilme = 2,
                DataLocacao = DateTime.Now,
                DevolucaoPrevista = DateTime.Now.AddDays(1)
            };
            var dataLocacao = controller.Post(locacao) as CreatedAtRouteResult;
            var resultadoLocacao = dataLocacao.Value as Locacoes;
            var devolucao = new LocacoesVM
            {
                Id = resultadoLocacao.Id,
                IdCliente = resultadoLocacao.IdCliente,
                IdFilme = resultadoLocacao.IdFilme,
                DataLocacao = resultadoLocacao.DataLocacao,
                DevolucaoPrevista = resultadoLocacao.DevolucaoPrevista,
                Devolucao = DateTime.Now.AddDays(2)
            };

            // --> Efetua devolução
            await controller.Put(devolucao.Id, devolucao);
            var dadosDevolucao = await controller.GetById(devolucao.Id) as OkObjectResult;
            var resultadoDevolucao = dadosDevolucao.Value as LocacoesVM;

            // Assert
            Assert.NotNull(resultadoDevolucao);
            Assert.NotEmpty(resultadoDevolucao.Alerta);
        }

    }
}
