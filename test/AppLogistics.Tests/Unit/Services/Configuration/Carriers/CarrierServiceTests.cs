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
    public class CarrierServiceTests : IDisposable
    {
        private CarrierService service;
        private TestingContext context;
        private Carrier carrier;

        public CarrierServiceTests()
        {
            context = new TestingContext();
            service = new CarrierService(new UnitOfWork(new TestingContext(context)));

            context.Set<Carrier>().Add(carrier = ObjectsFactory.CreateCarrier());
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
            CarrierView actual = service.Get<CarrierView>(carrier.Id);
            CarrierView expected = Mapper.Map<CarrierView>(carrier);

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Nit, actual.Nit);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region GetViews()

        [Fact]
        public void GetViews_ReturnsCarrierViews()
        {
            CarrierView[] actual = service.GetViews().ToArray();
            CarrierView[] expected = context
                .Set<Carrier>()
                .ProjectTo<CarrierView>()
                .OrderByDescending(view => view.CreationDate)
                .ToArray();

            for (int i = 0; i < expected.Length || i < actual.Length; i++)
            {
                                Assert.Equal(expected[i].CreationDate, actual[i].CreationDate);
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Nit, actual[i].Nit);
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        #endregion

        #region Create(CarrierView view)

        [Fact]
        public void Create_Carrier()
        {
            CarrierView view = ObjectsFactory.CreateCarrierView(1);
            view.Id = 0;

            service.Create(view);

            Carrier actual = context.Set<Carrier>().AsNoTracking().Single(model => model.Id != carrier.Id);
            CarrierView expected = view;

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Nit, actual.Nit);
        }

        #endregion

        #region Edit(CarrierView view)

        [Fact]
        public void Edit_Carrier()
        {
            CarrierView view = ObjectsFactory.CreateCarrierView(carrier.Id);
            view.Name = "Name0";
            view.Nit = "Nit0";

            service.Edit(view);

            Carrier actual = context.Set<Carrier>().AsNoTracking().Single();
            Carrier expected = carrier;

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Nit, actual.Nit);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region Delete(String id)

        [Fact]
        public void Delete_Carrier()
        {
            service.Delete(carrier.Id);

            Assert.Empty(context.Set<Carrier>());
        }

        #endregion
    }
}
