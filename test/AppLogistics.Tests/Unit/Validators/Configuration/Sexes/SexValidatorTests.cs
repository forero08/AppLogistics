using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class SexValidatorTests : IDisposable
    {
        private SexValidator validator;
        private TestingContext context;
        private Sex sex;

        public SexValidatorTests()
        {
            context = new TestingContext();
            validator = new SexValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Sex>().Add(sex = ObjectsFactory.CreateSex());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(SexView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateSexView(1)));
        }

        [Fact]
        public void CanCreate_ValidSex()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateSexView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(SexView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateSexView(sex.Id)));
        }

        [Fact]
        public void CanEdit_ValidSex()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateSexView(sex.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
