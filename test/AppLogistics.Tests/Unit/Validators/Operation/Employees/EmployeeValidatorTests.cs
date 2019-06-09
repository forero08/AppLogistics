using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class EmployeeValidatorTests : IDisposable
    {
        private EmployeeValidator validator;
        private TestingContext context;
        private Employee employee;

        public EmployeeValidatorTests()
        {
            context = new TestingContext();
            validator = new EmployeeValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Employee>().Add(employee = ObjectsFactory.CreateEmployee());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(EmployeeView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateEmployeeCreateEditView(1)));
        }

        [Fact]
        public void CanCreate_ValidEmployee()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateEmployeeCreateEditView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanCreate(EmployeeView view)

        #region CanEdit(EmployeeView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateEmployeeCreateEditView(employee.Id)));
        }

        [Fact]
        public void CanEdit_ValidEmployee()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateEmployeeCreateEditView(employee.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanEdit(EmployeeView view)
    }
}
