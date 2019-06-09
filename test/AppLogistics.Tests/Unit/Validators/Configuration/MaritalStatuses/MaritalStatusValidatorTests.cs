using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class MaritalStatusValidatorTests : IDisposable
    {
        private MaritalStatusValidator validator;
        private TestingContext context;
        private MaritalStatus maritalStatus;

        public MaritalStatusValidatorTests()
        {
            context = new TestingContext();
            validator = new MaritalStatusValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<MaritalStatus>().Add(maritalStatus = ObjectsFactory.CreateMaritalStatus());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(MaritalStatusView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateMaritalStatusView(1)));
        }

        [Fact]
        public void CanCreate_ValidMaritalStatus()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateMaritalStatusView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanCreate(MaritalStatusView view)

        #region CanEdit(MaritalStatusView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateMaritalStatusView(maritalStatus.Id)));
        }

        [Fact]
        public void CanEdit_ValidMaritalStatus()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateMaritalStatusView(maritalStatus.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanEdit(MaritalStatusView view)
    }
}
