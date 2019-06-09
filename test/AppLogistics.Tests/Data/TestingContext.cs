using AppLogistics.Data.Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppLogistics.Tests
{
    public class TestingContext : Context
    {
        #region Tests

        protected DbSet<TestModel> TestModel { get; set; }

        #endregion Tests

        private string DatabaseName { get; }

        public TestingContext()
        {
            DatabaseName = Guid.NewGuid().ToString();
        }

        public TestingContext(TestingContext context)
        {
            DatabaseName = context.DatabaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(DatabaseName);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
