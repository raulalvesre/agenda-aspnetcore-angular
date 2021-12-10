using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Agenda.Domain.Models;
using Agenda.Domain.Core;
using Agenda.Infrastructure.Configurations.Base;
using Agenda.Domain.Models.Types;


namespace Agenda.Infrastructure.Configurations.Types
{
    public class InteractionTypeConfiguration : EnumerationClassConfiguration<InteractionType>
    {
        public override void Configure(EntityTypeBuilder<InteractionType> builder)
        {
            base.Configure(builder);

            builder.ToTable("Interaction Types");
        }

    }
}
