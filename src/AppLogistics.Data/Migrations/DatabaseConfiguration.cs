using AppLogistics.Components.Security;
using AppLogistics.Data.Core;
using AppLogistics.Data.Logging;
using AppLogistics.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AppLogistics.Data.Migrations
{
    public sealed class DatabaseConfiguration : IDisposable
    {
        private IUnitOfWork UnitOfWork { get; }
        private readonly DbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHasher _hasher;

        private readonly string roleTitleSysAdmin = "Sys_Admin";

        public DatabaseConfiguration(DbContext context, DbContext audit, IConfiguration configuration, IHasher hasher)
        {
            UnitOfWork = new UnitOfWork(context, audit == null ? null : new AuditLogger(audit, 0));
            _dbContext = context;
            _configuration = configuration;
            _hasher = hasher;
        }

        public void UpdateDatabase()
        {
            _dbContext.Database.Migrate();

            SeedData();
        }

        public void SeedData()
        {
            SeedPermissions();
            SeedRoles();

            SeedAccounts();
        }

        #region Administration

        private void SeedPermissions()
        {
            Permission[] permissions =
            {
                new Permission { Id = 1, Area = "Administration", Controller = "Accounts", Action = "Index" },
                new Permission { Id = 2, Area = "Administration", Controller = "Accounts", Action = "Create" },
                new Permission { Id = 3, Area = "Administration", Controller = "Accounts", Action = "Details" },
                new Permission { Id = 4, Area = "Administration", Controller = "Accounts", Action = "Edit" },

                new Permission { Id = 5, Area = "Administration", Controller = "Roles", Action = "Index" },
                new Permission { Id = 6, Area = "Administration", Controller = "Roles", Action = "Create" },
                new Permission { Id = 7, Area = "Administration", Controller = "Roles", Action = "Details" },
                new Permission { Id = 8, Area = "Administration", Controller = "Roles", Action = "Edit" },
                new Permission { Id = 9, Area = "Administration", Controller = "Roles", Action = "Delete" }
            };

            Permission[] currentPermissions = UnitOfWork.Select<Permission>().ToArray();
            foreach (Permission permission in currentPermissions)
            {
                if (permissions.All(perm => perm.Id != permission.Id))
                {
                    UnitOfWork.DeleteRange(UnitOfWork.Select<RolePermission>().Where(role => role.PermissionId == permission.Id));
                    UnitOfWork.Delete(permission);
                }
            }

            foreach (Permission permission in permissions)
            {
                if (currentPermissions.SingleOrDefault(perm => perm.Id == permission.Id) is Permission currentPermission)
                {
                    currentPermission.Controller = permission.Controller;
                    currentPermission.Action = permission.Action;
                    currentPermission.Area = permission.Area;

                    UnitOfWork.Update(currentPermission);
                }
                else
                {
                    UnitOfWork.Insert(permission);
                }
            }

            UnitOfWork.Commit();
        }

        private void SeedRoles()
        {
            if (!UnitOfWork.Select<Role>().Any(role => role.Title == roleTitleSysAdmin))
            {
                UnitOfWork.Insert(new Role { Title = roleTitleSysAdmin });
                UnitOfWork.Commit();
            }

            int adminRoleId = UnitOfWork.Select<Role>().Single(role => role.Title == roleTitleSysAdmin).Id;
            RolePermission[] currentPermissions = UnitOfWork
                .Select<RolePermission>()
                .Where(rolePermission => rolePermission.RoleId == adminRoleId)
                .ToArray();

            foreach (Permission permission in UnitOfWork.Select<Permission>())
            {
                if (currentPermissions.All(rolePermission => rolePermission.PermissionId != permission.Id))
                {
                    UnitOfWork.Insert(new RolePermission
                    {
                        RoleId = adminRoleId,
                        PermissionId = permission.Id
                    });
                }
            }

            UnitOfWork.Commit();
        }

        private void SeedAccounts()
        {
            Account[] accounts =
            {
                new Account
                {
                    Username = _configuration.GetValue<string>("UserAdmin:UserName"),
                    Passhash = _hasher.HashPassword(_configuration.GetValue<string>("UserAdmin:Password")),
                    Email = _configuration.GetValue<string>("UserAdmin:Email"),
                    IsLocked = false,

                    RoleId = UnitOfWork.Select<Role>().Single(role => role.Title == roleTitleSysAdmin).Id
                }
            };

            foreach (Account account in accounts)
            {
                if (UnitOfWork.Select<Account>().FirstOrDefault(model => model.Username == account.Username) is Account currentAccount)
                {
                    currentAccount.IsLocked = account.IsLocked;
                    currentAccount.RoleId = account.RoleId;

                    UnitOfWork.Update(currentAccount);
                }
                else
                {
                    UnitOfWork.Insert(account);
                }
            }

            UnitOfWork.Commit();
        }

        #endregion Administration

        public void Dispose()
        {
            UnitOfWork.Dispose();
            _dbContext.Dispose();
        }
    }
}
