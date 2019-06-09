using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class RateValidatorTests : IDisposable
    {
        private RateValidator validator;
        private TestingContext context;
        private Rate rate;

        public RateValidatorTests()
        {
            context = new TestingContext();
            validator = new RateValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Rate>().Add(rate = ObjectsFactory.CreateRate());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(RateView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateRateCreateEditView(1)));
        }

        [Fact]
        public void CanCreate_ValidRate()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateRateCreateEditView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(RateView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateRateCreateEditView(rate.Id)));
        }

        [Fact]
        public void CanEdit_ValidRate()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateRateCreateEditView(rate.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
