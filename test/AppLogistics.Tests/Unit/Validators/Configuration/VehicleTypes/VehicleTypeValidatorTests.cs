using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class VehicleTypeValidatorTests : IDisposable
    {
        private VehicleTypeValidator validator;
        private TestingContext context;
        private VehicleType vehicleType;

        public VehicleTypeValidatorTests()
        {
            context = new TestingContext();
            validator = new VehicleTypeValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<VehicleType>().Add(vehicleType = ObjectsFactory.CreateVehicleType());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(VehicleTypeView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateVehicleTypeView(1)));
        }

        [Fact]
        public void CanCreate_ValidVehicleType()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateVehicleTypeView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(VehicleTypeView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateVehicleTypeView(vehicleType.Id)));
        }

        [Fact]
        public void CanEdit_ValidVehicleType()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateVehicleTypeView(vehicleType.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
