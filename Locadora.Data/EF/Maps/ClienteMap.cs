using Locadora.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Locadora.Data.EF.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {

            //Table
            builder.ToTable(nameof(Cliente));

            //PK
            builder.HasKey(c => c.Id);

            //Columns
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Nome).IsRequired().HasColumnType("varchar(50)");
            builder.Property(c => c.CPF).IsRequired().HasColumnType("varchar(11)");

            //Index
            builder.HasIndex(c => c.CPF).IsUnique();

            //Relacionamentos

        }

    }
}
