using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class EducationLevelValidatorTests : IDisposable
    {
        private EducationLevelValidator validator;
        private TestingContext context;
        private EducationLevel educationLevel;

        public EducationLevelValidatorTests()
        {
            context = new TestingContext();
            validator = new EducationLevelValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<EducationLevel>().Add(educationLevel = ObjectsFactory.CreateEducationLevel());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(EducationLevelView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateEducationLevelView(1)));
        }

        [Fact]
        public void CanCreate_ValidEducationLevel()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateEducationLevelView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(EducationLevelView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateEducationLevelView(educationLevel.Id)));
        }

        [Fact]
        public void CanEdit_ValidEducationLevel()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateEducationLevelView(educationLevel.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
