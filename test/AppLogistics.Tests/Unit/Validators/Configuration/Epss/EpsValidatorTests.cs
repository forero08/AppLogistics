using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class EpsValidatorTests : IDisposable
    {
        private EpsValidator validator;
        private TestingContext context;
        private Eps eps;

        public EpsValidatorTests()
        {
            context = new TestingContext();
            validator = new EpsValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Eps>().Add(eps = ObjectsFactory.CreateEps());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(EpsView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateEpsView(1)));
        }

        [Fact]
        public void CanCreate_ValidEps()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateEpsView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(EpsView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateEpsView(eps.Id)));
        }

        [Fact]
        public void CanEdit_ValidEps()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateEpsView(eps.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
