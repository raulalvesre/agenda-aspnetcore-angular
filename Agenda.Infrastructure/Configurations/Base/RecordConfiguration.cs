using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Agenda.Domain.Core;

namespace Agenda.Infrastructure.Configurations.Base
{
    public class RecordConfiguration<T> : IEntityTypeConfiguration<T> where T : Record
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.CreationDate).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(r => r.LastUpdateDate).HasColumnType("datetime2");
        }

    }
}
