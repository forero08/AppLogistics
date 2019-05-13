using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AppLogistics.Services.Tests
{
    public class ProductServiceTests : IDisposable
    {
        private ProductService service;
        private TestingContext context;
        private Product product;

        public ProductServiceTests()
        {
            context = new TestingContext();
            service = new ProductService(new UnitOfWork(new TestingContext(context)));

            context.Set<Product>().Add(product = ObjectsFactory.CreateProduct());
            context.SaveChanges();
        }

        public void Dispose()
        {
            service.Dispose();
            context.Dispose();
        }

        #region Get<TView>(String id)

        [Fact]
        public void Get_ReturnsViewById()
        {
            ProductView actual = service.Get<ProductView>(product.Id);
            ProductView expected = Mapper.Map<ProductView>(product);

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region GetViews()

        [Fact]
        public void GetViews_ReturnsProductViews()
        {
            ProductView[] actual = service.GetViews().ToArray();
            ProductView[] expected = context
                .Set<Product>()
                .ProjectTo<ProductView>()
                .OrderByDescending(view => view.CreationDate)
                .ToArray();

            for (int i = 0; i < expected.Length || i < actual.Length; i++)
            {
                                Assert.Equal(expected[i].CreationDate, actual[i].CreationDate);
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        #endregion

        #region Create(ProductView view)

        [Fact]
        public void Create_Product()
        {
            ProductView view = ObjectsFactory.CreateProductView(1);
            view.Id = 0;

            service.Create(view);

            Product actual = context.Set<Product>().AsNoTracking().Single(model => model.Id != product.Id);
            ProductView expected = view;

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
        }

        #endregion

        #region Edit(ProductView view)

        [Fact]
        public void Edit_Product()
        {
            ProductView view = ObjectsFactory.CreateProductView(product.Id);
            view.Name = "Name0";

            service.Edit(view);

            Product actual = context.Set<Product>().AsNoTracking().Single();
            Product expected = product;

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region Delete(String id)

        [Fact]
        public void Delete_Product()
        {
            service.Delete(product.Id);

            Assert.Empty(context.Set<Product>());
        }

        #endregion
    }
}
