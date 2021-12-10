using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Agenda.Domain.Models;
using Agenda.Infrastructure.Configurations.Base;
using Agenda.Domain.Models.Types;


namespace Agenda.Infrastructure.Configurations
{
    public class ContactTelephoneConfiguration : RecordConfiguration<ContactTelephone>
    {
        public override void Configure(EntityTypeBuilder<ContactTelephone> builder)
        {
            base.Configure(builder);

            builder.ToTable("Contact Telephones");
            builder.Property(t => t.Description).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Ddd).HasColumnType("tinyint").IsRequired();
            builder.Property(t => t.TelephoneOnlyNumbers).HasMaxLength(9).IsRequired();
            builder.Property(t => t.TelephoneFormatted).HasMaxLength(15).IsRequired();

            builder.HasOne(t => t.TelephoneType)
                .WithMany()
                .HasForeignKey(t => t.TelephoneTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
