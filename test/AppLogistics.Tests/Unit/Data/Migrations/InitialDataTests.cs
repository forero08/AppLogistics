using AppLogistics.Components.Security;
using AppLogistics.Objects;
using AppLogistics.Tests;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AppLogistics.Data.Migrations.Tests
{
    public class InitialDataTests : IDisposable
    {
        private DatabaseConfiguration dbConfiguration;
        private TestingContext context;

        public InitialDataTests()
        {
            context = new TestingContext();
            dbConfiguration = new DatabaseConfiguration(context, null, Substitute.For<IConfiguration>(), Substitute.For<IHasher>());
            dbConfiguration.SeedData();
        }

        public void Dispose()
        {
            dbConfiguration.Dispose();
            context.Dispose();
        }

        #region Roles

        [Fact]
        public void RolesTable_HasSysAdmin()
        {
            Assert.Single(context.Set<Role>(), role => role.Title == "Sys_Admin");
        }

        #endregion

        #region Accounts

        [Fact(Skip = "Need to mock IConfiguration properly")]
        public void AccountsTable_HasAdmin()
        {
            Assert.Single(context.Set<Account>(), account => account.Username == "Admin" && account.Role.Title == "Sys_Admin");
        }

        #endregion

        #region Permissions

        [Theory]
        [InlineData("Administration", "Accounts", "Index")]
        [InlineData("Administration", "Accounts", "Create")]
        [InlineData("Administration", "Accounts", "Details")]
        [InlineData("Administration", "Accounts", "Edit")]

        [InlineData("Administration", "Roles", "Index")]
        [InlineData("Administration", "Roles", "Create")]
        [InlineData("Administration", "Roles", "Details")]
        [InlineData("Administration", "Roles", "Edit")]
        [InlineData("Administration", "Roles", "Delete")]
        public void PermissionsTable_HasPermission(string area, string controller, string action)
        {
            Assert.Single(context.Set<Permission>(), permission =>
                permission.Controller == controller
                && permission.Action == action
                && permission.Area == area);
        }

        [Fact]
        public void PermissionsTable_HasExactNumberOfPermissions()
        {
            int actual = context.Set<Permission>().Count();
            int expected = 14;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region RolePermissions

        [Fact]
        public void RolesPermissionsTable_HasAllSysAdminPermissions()
        {
            IEnumerable<int> expected = context
                .Set<Permission>()
                .Select(permission => permission.Id)
                .OrderBy(permissionId => permissionId);

            IEnumerable<int> actual = context
                .Set<RolePermission>()
                .Where(permission => permission.Role.Title == "Sys_Admin")
                .Select(rolePermission => rolePermission.PermissionId)
                .OrderBy(permissionId => permissionId);

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
