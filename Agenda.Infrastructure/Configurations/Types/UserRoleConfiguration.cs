using Agenda.Domain.Models;
using Agenda.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Agenda.Domain.Models.Types;


namespace Agenda.Infrastructure.Configurations.Types.Types
{
    public class UserRoleConfiguration : EnumerationClassConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);

            builder.ToTable("User Roles");
        }

    }
}
