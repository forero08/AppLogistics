using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class SectorValidatorTests : IDisposable
    {
        private SectorValidator validator;
        private TestingContext context;
        private Sector sector;

        public SectorValidatorTests()
        {
            context = new TestingContext();
            validator = new SectorValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Sector>().Add(sector = ObjectsFactory.CreateSector());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(SectorView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateSectorView(1)));
        }

        [Fact]
        public void CanCreate_ValidSector()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateSectorView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(SectorView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateSectorView(sector.Id)));
        }

        [Fact]
        public void CanEdit_ValidSector()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateSectorView(sector.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
