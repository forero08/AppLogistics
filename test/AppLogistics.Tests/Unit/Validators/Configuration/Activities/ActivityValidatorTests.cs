using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class ActivityValidatorTests : IDisposable
    {
        private ActivityValidator validator;
        private TestingContext context;
        private Activity activity;

        public ActivityValidatorTests()
        {
            context = new TestingContext();
            validator = new ActivityValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Activity>().Add(activity = ObjectsFactory.CreateActivity());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(ActivityView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateActivityView(1)));
        }

        [Fact]
        public void CanCreate_ValidActivity()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateActivityView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanCreate(ActivityView view)

        #region CanEdit(ActivityView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateActivityView(activity.Id)));
        }

        [Fact]
        public void CanEdit_ValidActivity()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateActivityView(activity.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanEdit(ActivityView view)
    }
}
