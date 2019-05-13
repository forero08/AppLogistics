using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class AfpValidatorTests : IDisposable
    {
        private AfpValidator validator;
        private TestingContext context;
        private Afp afp;

        public AfpValidatorTests()
        {
            context = new TestingContext();
            validator = new AfpValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Afp>().Add(afp = ObjectsFactory.CreateAfp());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(AfpView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateAfpView(1)));
        }

        [Fact]
        public void CanCreate_ValidAfp()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateAfpView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(AfpView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateAfpView(afp.Id)));
        }

        [Fact]
        public void CanEdit_ValidAfp()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateAfpView(afp.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
