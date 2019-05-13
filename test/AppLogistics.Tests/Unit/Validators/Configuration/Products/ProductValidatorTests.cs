using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class ProductValidatorTests : IDisposable
    {
        private ProductValidator validator;
        private TestingContext context;
        private Product product;

        public ProductValidatorTests()
        {
            context = new TestingContext();
            validator = new ProductValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Product>().Add(product = ObjectsFactory.CreateProduct());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(ProductView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateProductView(1)));
        }

        [Fact]
        public void CanCreate_ValidProduct()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateProductView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(ProductView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateProductView(product.Id)));
        }

        [Fact]
        public void CanEdit_ValidProduct()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateProductView(product.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
