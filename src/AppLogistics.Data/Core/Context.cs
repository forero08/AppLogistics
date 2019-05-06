using AppLogistics.Components.Mvc;
using AppLogistics.Data.Mapping;
using AppLogistics.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Reflection;

namespace AppLogistics.Data.Core
{
    public class Context : DbContext
    {
        #region Administration

        protected DbSet<Role> Role { get; set; }
        protected DbSet<Account> Account { get; set; }
        protected DbSet<Permission> Permission { get; set; }
        protected DbSet<RolePermission> RolePermission { get; set; }

        #endregion Administration

        #region Configuration

        protected DbSet<Afp> Afp { get; set; }
        protected DbSet<Eps> Eps{ get; set; }

        #endregion

        #region System

        protected DbSet<AuditLog> AuditLog { get; set; }

        #endregion System

        static Context()
        {
            ObjectMapper.MapObjects();
        }

        protected Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (PropertyInfo property in entity.ClrType.GetProperties())
                {
                    if (property.GetCustomAttribute<IndexAttribute>(false) is IndexAttribute index)
                    {
                        modelBuilder.Entity(entity.ClrType).HasIndex(property.Name).IsUnique(index.IsUnique);
                    }
                }
            }

            modelBuilder.Entity<Permission>().Property(model => model.Id).ValueGeneratedNever();
            foreach (IMutableForeignKey key in modelBuilder.Model.GetEntityTypes().SelectMany(entity => entity.GetForeignKeys()))
            {
                key.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
