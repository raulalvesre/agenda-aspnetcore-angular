using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Agenda.Domain.Models;
using Agenda.Infrastructure.Configurations.Base;

namespace Agenda.Infrastructure.Configurations
{
    public class ContactConfiguration : RecordConfiguration<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> builder)
        {
            base.Configure(builder);

            builder.ToTable("Contacts");
            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();

            builder.HasOne(c => c.Owner)
                   .WithMany()
                   .HasForeignKey(c => c.OwnerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Telephones)
                   .WithOne(t => t.Contact)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
