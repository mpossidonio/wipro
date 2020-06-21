using Locadora.Controllers;
using Locadora.Data.EF;
using Locadora.Data.EF.Repositories;
using Locadora.Domain.Contracts.Repositories;
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
    public class FilmesUnitTestController
    {
        public static LocadoraDataContext locadoraDataContext { get; }

        private static IFilmeRepository filmeRepository;

        static FilmesUnitTestController()
        {
            DbContextOptions<LocadoraDataContext> options;
            var builder = new DbContextOptionsBuilder<LocadoraDataContext>();
            builder.UseInMemoryDatabase(databaseName: "Locadora");
            options = builder.Options;
            locadoraDataContext = new LocadoraDataContext(options);
            DbInitializer.Initializer(locadoraDataContext);

            filmeRepository = new FilmeRepositoryEF(locadoraDataContext);
        }


        // ---------------------------------------------------------------------------
        // Inicio dos Testes

        [Fact]
        public async Task GetAllFilmes_Return_OkResult()
        {
            // Arrange
            var controller = new FilmesController(filmeRepository);

            // Act
            var data = await controller.GetAll();

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public async Task GetFilmesById_Return_OkResult()
        {
            // Arrange
            var controller = new FilmesController(filmeRepository);

            // Act
            var data = await controller.GetById(1);

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public async Task GetFilmesByName_Return_OkResult()
        {
            // Arrange
            var controller = new FilmesController(filmeRepository);

            // Act
            var data = await controller.GetByName("Era uma vez no oeste");

            // Assert
            Assert.Equal(typeof(OkObjectResult), data.GetType());
        }

        [Fact]
        public void PostFilmes_Return_CreatedAtRouteResult()
        {
            // Arrange
            var controller = new FilmesController(filmeRepository);

            // Act
            var filme = new FilmeVM
            {
                Titulo = "Curtindo a Vida Adoidado",
                TituloOriginal = "Ferris Bullers Day Off",
                Ano = 1986
            };

            var data = controller.Post(filme);

            // Assert
            Assert.Equal(typeof(CreatedAtRouteResult), data.GetType());
        }

        [Fact]
        public async Task PutFilmes_Return_NoContentResult()
        {
            // Arrange
            var controller = new FilmesController(filmeRepository);

            // Act
            var filme = new FilmeVM
            {
                Titulo = "Era Uma Vez no Oeste",
                TituloOriginal = "Once Upon a Time in the West",
                Ano = 1968
            };

            var data = await controller.Put(1, filme);

            // Assert
            Assert.Equal(typeof(NoContentResult), data.GetType());
        }

    }
}
