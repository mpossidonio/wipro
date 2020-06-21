using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Data.EF
{
    public class LocadoraDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LocadoraDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public LocadoraDataContext(DbContextOptions<LocadoraDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Locadora");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Maps.FilmeMap());
            modelBuilder.ApplyConfiguration(new Maps.LocacoesMap());
            modelBuilder.ApplyConfiguration(new Maps.ClienteMap());
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Locacoes> Locacoes { get; set; }
    }
}
