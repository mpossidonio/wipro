using Locadora.Data.EF;
using Locadora.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Locadora.Domain.Contracts.Repositories;
using Locadora.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Locadora.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.XUnitTest
{
    public class ClientesUnitTestController
    {
        public static LocadoraDataContext locadoraDataContext { get; }

        private static IClienteRepository clienteRepository;

        static ClientesUnitTestController()
        {
            //locadoraDataContext = new DbContextOptionsBuilder<LocadoraDataContext>().UseInMemoryDatabase(databaseName: "Locadora");

            DbContextOptions<LocadoraDataContext> options;
            var builder = new DbContextOptionsBuilder<LocadoraDataContext>();
            builder.UseInMemoryDatabase(databaseName: "Locadora");
            options = builder.Options;
            locadoraDataContext = new LocadoraDataContext(options);
            DbInitializer.Initializer(locadoraDataContext);

            clienteRepository = new ClienteRepositoryEF(locadoraDataContext);
        }


        // ---------------------------------------------------------------------------
        // Inicio dos Testes

        [Fact]
        public async Task GetAllClientes_Return_OkResult()
        {
            // Arrange
            var controller = new ClientesController(clienteRepository);

            // Act
            var data = await controller.GetAll();

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public async Task GetClientesById_Return_OkResult()
        {
            // Arrange
            var controller = new ClientesController(clienteRepository);

            // Act
            var data = await controller.GetById(1);

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public async Task GetClientesByName_Return_OkResult()
        {
            // Arrange
            var controller = new ClientesController(clienteRepository);

            // Act
            var data = await controller.GetByName("Cliente 1");

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public void PostClientes_Return_CreatedAtRouteResult()
        {
            // Arrange
            var controller = new ClientesController(clienteRepository);

            // Act
            var cliente = new ClienteVM
            {
                Nome = "Teste Post",
                CPF = "99999999999"
            };

            var data = controller.Post(cliente);
            
            // Assert
            Assert.Equal(typeof(CreatedAtRouteResult), data.GetType());
        }

        [Fact]
        public async Task PutClientes_Return_NoContentResult()
        {
            // Arrange
            var controller = new ClientesController(clienteRepository);

            // Act
            var cliente = new ClienteVM
            {
                Nome = "Cliente 1 Alterado",
                CPF = "11111111111"
            };

            var data = await controller.Put(1, cliente);

            // Assert
            Assert.Equal(typeof(NoContentResult), data.GetType());
        }

    }
}
