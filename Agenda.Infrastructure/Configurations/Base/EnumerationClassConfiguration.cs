using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Configurations.Base
{
    public class EnumerationClassConfiguration<T> : IEntityTypeConfiguration<T> where T : Enumeration
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(eC => eC.Id).ValueGeneratedNever();
            builder.Property(eC => eC.Name).HasMaxLength(200).IsRequired();

            builder.HasData(Enumeration.GetAll<T>());

            builder.HasKey(eC => eC.Id);
        }
    }
}
