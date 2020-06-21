using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Data.EF.Maps
{
    public class LocacoesMap : IEntityTypeConfiguration<Locacoes>
    {
        public void Configure(EntityTypeBuilder<Locacoes> builder)
        {

            //Table
            builder.ToTable(nameof(Locacoes));

            //PK
            builder.HasKey(c => c.Id);

            //Columns
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.IdCliente).IsRequired().HasColumnType("int");
            builder.Property(c => c.IdFilme).IsRequired().HasColumnType("int");
            builder.Property(c => c.DataLocacao).IsRequired().HasColumnType("datetime");
            builder.Property(c => c.DevolucaoPrevista).IsRequired().HasColumnType("datetime");
            builder.Property(c => c.Devolucao).HasColumnType("datetime");
            
            //Relacionamentos


        }
    }
}
