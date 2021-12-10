using System;
using Microsoft.EntityFrameworkCore;

using Agenda.Domain.Core;
using Agenda.Domain.Models;
using System.Threading.Tasks;
using System.Threading;

namespace Agenda.Infrastructure
{
    public class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contatos { get; set; }
        public DbSet<ContactTelephone> Telephones { get; set; }
        public DbSet<Interaction> Interactions { get; set; }

        private static string _connStr = @"
            Server=127.0.0.1,1433;
            Database=phonebook;
            User Id=SA;
            Password=@eosqlas432
        ";

        public ApplicationContext() : base() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connStr);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is Record record)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            record.CreationDate = now;
                            record.LastUpdateDate = null;
                            break;
                        case EntityState.Modified:
                            record.LastUpdateDate = now;
                            break;
                    }
                }
            }

            return await base.SaveChangesAsync(true, cancellationToken);
        }

    }
}
