using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class CountryValidatorTests : IDisposable
    {
        private CountryValidator validator;
        private TestingContext context;
        private Country country;

        public CountryValidatorTests()
        {
            context = new TestingContext();
            validator = new CountryValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Country>().Add(country = ObjectsFactory.CreateCountry());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(CountryView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateCountryView(1)));
        }

        [Fact]
        public void CanCreate_ValidCountry()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateCountryView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(CountryView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateCountryView(country.Id)));
        }

        [Fact]
        public void CanEdit_ValidCountry()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateCountryView(country.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
