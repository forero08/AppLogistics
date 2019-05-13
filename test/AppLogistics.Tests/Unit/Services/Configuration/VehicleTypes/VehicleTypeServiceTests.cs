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
    public class VehicleTypeServiceTests : IDisposable
    {
        private VehicleTypeService service;
        private TestingContext context;
        private VehicleType vehicleType;

        public VehicleTypeServiceTests()
        {
            context = new TestingContext();
            service = new VehicleTypeService(new UnitOfWork(new TestingContext(context)));

            context.Set<VehicleType>().Add(vehicleType = ObjectsFactory.CreateVehicleType());
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
            VehicleTypeView actual = service.Get<VehicleTypeView>(vehicleType.Id);
            VehicleTypeView expected = Mapper.Map<VehicleTypeView>(vehicleType);

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region GetViews()

        [Fact]
        public void GetViews_ReturnsVehicleTypeViews()
        {
            VehicleTypeView[] actual = service.GetViews().ToArray();
            VehicleTypeView[] expected = context
                .Set<VehicleType>()
                .ProjectTo<VehicleTypeView>()
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

        #region Create(VehicleTypeView view)

        [Fact]
        public void Create_VehicleType()
        {
            VehicleTypeView view = ObjectsFactory.CreateVehicleTypeView(1);
            view.Id = 0;

            service.Create(view);

            VehicleType actual = context.Set<VehicleType>().AsNoTracking().Single(model => model.Id != vehicleType.Id);
            VehicleTypeView expected = view;

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
        }

        #endregion

        #region Edit(VehicleTypeView view)

        [Fact]
        public void Edit_VehicleType()
        {
            VehicleTypeView view = ObjectsFactory.CreateVehicleTypeView(vehicleType.Id);
            view.Name = "Name0";

            service.Edit(view);

            VehicleType actual = context.Set<VehicleType>().AsNoTracking().Single();
            VehicleType expected = vehicleType;

            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region Delete(String id)

        [Fact]
        public void Delete_VehicleType()
        {
            service.Delete(vehicleType.Id);

            Assert.Empty(context.Set<VehicleType>());
        }

        #endregion
    }
}
