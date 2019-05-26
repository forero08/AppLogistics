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

        [InlineData("Configuration", "Activities", "Index")]
        [InlineData("Configuration", "Activities", "Create")]
        [InlineData("Configuration", "Activities", "Details")]
        [InlineData("Configuration", "Activities", "Edit")]
        [InlineData("Configuration", "Activities", "Delete")]

        [InlineData("Configuration", "Afps", "Index")]
        [InlineData("Configuration", "Afps", "Create")]
        [InlineData("Configuration", "Afps", "Details")]
        [InlineData("Configuration", "Afps", "Edit")]
        [InlineData("Configuration", "Afps", "Delete")]

        [InlineData("Configuration", "BranchOffices", "Index")]
        [InlineData("Configuration", "BranchOffices", "Create")]
        [InlineData("Configuration", "BranchOffices", "Details")]
        [InlineData("Configuration", "BranchOffices", "Edit")]
        [InlineData("Configuration", "BranchOffices", "Delete")]

        [InlineData("Configuration", "Carriers", "Index")]
        [InlineData("Configuration", "Carriers", "Create")]
        [InlineData("Configuration", "Carriers", "Details")]
        [InlineData("Configuration", "Carriers", "Edit")]
        [InlineData("Configuration", "Carriers", "Delete")]

        [InlineData("Configuration", "Clients", "Index")]
        [InlineData("Configuration", "Clients", "Create")]
        [InlineData("Configuration", "Clients", "Details")]
        [InlineData("Configuration", "Clients", "Edit")]
        [InlineData("Configuration", "Clients", "Delete")]

        [InlineData("Configuration", "DocumentTypes", "Index")]
        [InlineData("Configuration", "DocumentTypes", "Create")]
        [InlineData("Configuration", "DocumentTypes", "Details")]
        [InlineData("Configuration", "DocumentTypes", "Edit")]
        [InlineData("Configuration", "DocumentTypes", "Delete")]

        [InlineData("Configuration", "Epss", "Index")]
        [InlineData("Configuration", "Epss", "Create")]
        [InlineData("Configuration", "Epss", "Details")]
        [InlineData("Configuration", "Epss", "Edit")]
        [InlineData("Configuration", "Epss", "Delete")]

        [InlineData("Configuration", "MaritalStatuses", "Index")]
        [InlineData("Configuration", "MaritalStatuses", "Create")]
        [InlineData("Configuration", "MaritalStatuses", "Details")]
        [InlineData("Configuration", "MaritalStatuses", "Edit")]
        [InlineData("Configuration", "MaritalStatuses", "Delete")]

        [InlineData("Configuration", "Products", "Index")]
        [InlineData("Configuration", "Products", "Create")]
        [InlineData("Configuration", "Products", "Details")]
        [InlineData("Configuration", "Products", "Edit")]
        [InlineData("Configuration", "Products", "Delete")]

        [InlineData("Configuration", "VehicleTypes", "Index")]
        [InlineData("Configuration", "VehicleTypes", "Create")]
        [InlineData("Configuration", "VehicleTypes", "Details")]
        [InlineData("Configuration", "VehicleTypes", "Edit")]
        [InlineData("Configuration", "VehicleTypes", "Delete")]

        [InlineData("Operation", "Employees", "Index")]
        [InlineData("Operation", "Employees", "Create")]
        [InlineData("Operation", "Employees", "Details")]
        [InlineData("Operation", "Employees", "Edit")]
        [InlineData("Operation", "Employees", "Delete")]
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
            int expected = 64;

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
