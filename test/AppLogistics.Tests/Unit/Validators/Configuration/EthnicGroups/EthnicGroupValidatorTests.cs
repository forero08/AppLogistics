using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class EthnicGroupValidatorTests : IDisposable
    {
        private EthnicGroupValidator validator;
        private TestingContext context;
        private EthnicGroup ethnicGroup;

        public EthnicGroupValidatorTests()
        {
            context = new TestingContext();
            validator = new EthnicGroupValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<EthnicGroup>().Add(ethnicGroup = ObjectsFactory.CreateEthnicGroup());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(EthnicGroupView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateEthnicGroupView(1)));
        }

        [Fact]
        public void CanCreate_ValidEthnicGroup()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateEthnicGroupView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(EthnicGroupView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateEthnicGroupView(ethnicGroup.Id)));
        }

        [Fact]
        public void CanEdit_ValidEthnicGroup()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateEthnicGroupView(ethnicGroup.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
