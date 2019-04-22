using AppLogistics.Tests;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace AppLogistics.Data.Core.Tests
{
    public class QueryTests : IDisposable
    {
        private TestingContext context;
        private Query<TestModel> select;

        public QueryTests()
        {
            context = new TestingContext();
            select = new Query<TestModel>(context.Set<TestModel>());

            context.RemoveRange(context.Set<TestModel>());
            context.Add(ObjectsFactory.CreateTestModel());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        #region ElementType

        [Fact]
        public void ElementType_IsModelType()
        {
            object actual = (select as IQueryable).ElementType;
            object expected = typeof(TestModel);

            Assert.Same(expected, actual);
        }

        #endregion

        #region Expression

        [Fact]
        public void Expression_IsSetsExpression()
        {
            DbSet<TestModel> set = Substitute.For<DbSet<TestModel>, IQueryable>();
            TestingContext testingContext = Substitute.For<TestingContext>();
            ((IQueryable)set).Expression.Returns(Expression.Empty());
            testingContext.Set<TestModel>().Returns(set);

            select = new Query<TestModel>(testingContext.Set<TestModel>());

            object actual = ((IQueryable)select).Expression;
            object expected = ((IQueryable)set).Expression;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Provider

        [Fact]
        public void Provider_IsSetsProvider()
        {
            object expected = (context.Set<TestModel>() as IQueryable).Provider;
            object actual = (select as IQueryable).Provider;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Select<TResult>(Expression<Func<TModel, TResult>> selector)

        [Fact]
        public void Select_Selects()
        {
            IEnumerable<int> expected = context.Set<TestModel>().Select(model => model.Id);
            IEnumerable<int> actual = select.Select(model => model.Id);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Where(Expression<Func<TModel, Boolean>> predicate)

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Where_Filters(bool predicate)
        {
            IEnumerable<TestModel> expected = context.Set<TestModel>().Where(model => predicate);
            IEnumerable<TestModel> actual = select.Where(model => predicate);

            Assert.Equal(expected, actual);
        }

        #endregion

        #region To<TView>()

        [Fact]
        public void To_ProjectsSet()
        {
            IEnumerable<int> expected = context.Set<TestModel>().ProjectTo<TestView>().Select(view => view.Id).ToArray();
            IEnumerable<int> actual = select.To<TestView>().Select(view => view.Id).ToArray();

            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetEnumerator()

        [Fact]
        public void GetEnumerator_ReturnsSetEnumerator()
        {
            IEnumerable<TestModel> expected = context.Set<TestModel>();
            IEnumerable<TestModel> actual = select.ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEnumerator_ReturnsSameEnumerator()
        {
            IEnumerable<TestModel> expected = context.Set<TestModel>();
            IEnumerable<TestModel> actual = select;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
