using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Agenda.Domain.Models;
using Agenda.Infrastructure.Configurations.Base;

namespace Agenda.Infrastructure.Configurations
{
    public class InteractionConfiguration : RecordConfiguration<Interaction>
    {

        public override void Configure(EntityTypeBuilder<Interaction> builder)
        {
            builder.ToTable("Interactions");
            builder.Property(i => i.Message).HasMaxLength(200);

            builder.HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.InteractionType)
                   .WithMany()
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
