using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class CarrierValidatorTests : IDisposable
    {
        private CarrierValidator validator;
        private TestingContext context;
        private Carrier carrier;

        public CarrierValidatorTests()
        {
            context = new TestingContext();
            validator = new CarrierValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Carrier>().Add(carrier = ObjectsFactory.CreateCarrier());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(CarrierView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateCarrierView(1)));
        }

        [Fact]
        public void CanCreate_ValidCarrier()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateCarrierView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(CarrierView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateCarrierView(carrier.Id)));
        }

        [Fact]
        public void CanEdit_ValidCarrier()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateCarrierView(carrier.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
