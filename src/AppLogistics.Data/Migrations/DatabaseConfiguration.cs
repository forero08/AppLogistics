﻿using AppLogistics.Components.Security;
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
                new Permission { Id = 9, Area = "Administration", Controller = "Roles", Action = "Delete" },

                new Permission { Id = 10, Area = "Configuration", Controller = "Activities", Action = "Index" },
                new Permission { Id = 11, Area = "Configuration", Controller = "Activities", Action = "Create" },
                new Permission { Id = 12, Area = "Configuration", Controller = "Activities", Action = "Details" },
                new Permission { Id = 13, Area = "Configuration", Controller = "Activities", Action = "Edit" },
                new Permission { Id = 14, Area = "Configuration", Controller = "Activities", Action = "Delete" },

                new Permission { Id = 15, Area = "Configuration", Controller = "Afps", Action = "Index" },
                new Permission { Id = 16, Area = "Configuration", Controller = "Afps", Action = "Create" },
                new Permission { Id = 17, Area = "Configuration", Controller = "Afps", Action = "Details" },
                new Permission { Id = 18, Area = "Configuration", Controller = "Afps", Action = "Edit" },
                new Permission { Id = 19, Area = "Configuration", Controller = "Afps", Action = "Delete" },

                new Permission { Id = 20, Area = "Configuration", Controller = "DocumentTypes", Action = "Index" },
                new Permission { Id = 21, Area = "Configuration", Controller = "DocumentTypes", Action = "Create" },
                new Permission { Id = 22, Area = "Configuration", Controller = "DocumentTypes", Action = "Details" },
                new Permission { Id = 23, Area = "Configuration", Controller = "DocumentTypes", Action = "Edit" },
                new Permission { Id = 24, Area = "Configuration", Controller = "DocumentTypes", Action = "Delete" },

                new Permission { Id = 25, Area = "Configuration", Controller = "Epss", Action = "Index" },
                new Permission { Id = 26, Area = "Configuration", Controller = "Epss", Action = "Create" },
                new Permission { Id = 27, Area = "Configuration", Controller = "Epss", Action = "Details" },
                new Permission { Id = 28, Area = "Configuration", Controller = "Epss", Action = "Edit" },
                new Permission { Id = 29, Area = "Configuration", Controller = "Epss", Action = "Delete" },
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

        public void Dispose()
        {
            UnitOfWork.Dispose();
            _dbContext.Dispose();
        }
    }
}
