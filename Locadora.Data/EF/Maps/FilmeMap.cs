using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Data.EF.Maps
{
    public class FilmeMap : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {

            //Table
            builder.ToTable(nameof(Filme));

            //PK
            builder.HasKey(c => c.Id);

            //Columns
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Titulo).IsRequired().HasColumnType("varchar(50)");
            builder.Property(c => c.Ano).IsRequired().HasColumnType("smallint");
            builder.Property(c => c.TituloOriginal).IsRequired().HasColumnType("varchar(50)");
            builder.Property(c => c.NoCatalogo).HasColumnType("tinyint");

            //Relacionamentos


        }
    }
}