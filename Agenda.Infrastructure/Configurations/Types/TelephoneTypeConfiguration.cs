using Agenda.Domain.Models;
using Agenda.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Agenda.Domain.Models.Types;


namespace Agenda.Infrastructure.Configurations.Types
{
    public class TelephoneTypeConfiguration : EnumerationClassConfiguration<TelephoneType>
    {

        public override void Configure(EntityTypeBuilder<TelephoneType> builder)
        {
            base.Configure(builder);

            builder.ToTable("Telephone Types");
        }

    }
}
