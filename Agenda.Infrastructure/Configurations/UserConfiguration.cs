
using Agenda.Domain.Models;
using Agenda.Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Configurations
{
    public class UserConfiguration : RecordConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.Property(u => u.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Username)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
