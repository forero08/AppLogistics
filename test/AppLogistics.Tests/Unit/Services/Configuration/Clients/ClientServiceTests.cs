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
    public class ClientServiceTests : IDisposable
    {
        private ClientService service;
        private TestingContext context;
        private Client client;

        public ClientServiceTests()
        {
            context = new TestingContext();
            service = new ClientService(new UnitOfWork(new TestingContext(context)));

            context.Set<Client>().Add(client = ObjectsFactory.CreateClient());
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
            ClientView actual = service.Get<ClientView>(client.Id);
            ClientView expected = Mapper.Map<ClientView>(client);

            Assert.Equal(expected.BranchOfficeId, actual.BranchOfficeId);
            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Contact, actual.Contact);
            Assert.Equal(expected.Phone, actual.Phone);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Nit, actual.Nit);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region GetViews()

        [Fact]
        public void GetViews_ReturnsClientViews()
        {
            ClientView[] actual = service.GetViews().ToArray();
            ClientView[] expected = context
                .Set<Client>()
                .ProjectTo<ClientView>()
                .OrderByDescending(view => view.CreationDate)
                .ToArray();

            for (int i = 0; i < expected.Length || i < actual.Length; i++)
            {
                                Assert.Equal(expected[i].BranchOfficeId, actual[i].BranchOfficeId);
                Assert.Equal(expected[i].CreationDate, actual[i].CreationDate);
                Assert.Equal(expected[i].Address, actual[i].Address);
                Assert.Equal(expected[i].Contact, actual[i].Contact);
                Assert.Equal(expected[i].Phone, actual[i].Phone);
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Nit, actual[i].Nit);
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        #endregion

        #region Create(ClientView view)

        [Fact]
        public void Create_Client()
        {
            ClientView view = ObjectsFactory.CreateClientView(1);
            view.Id = 0;

            service.Create(view);

            Client actual = context.Set<Client>().AsNoTracking().Single(model => model.Id != client.Id);
            ClientView expected = view;

            Assert.Equal(expected.BranchOfficeId, actual.BranchOfficeId);
            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Contact, actual.Contact);
            Assert.Equal(expected.Phone, actual.Phone);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Nit, actual.Nit);
        }

        #endregion

        #region Edit(ClientView view)

        [Fact]
        public void Edit_Client()
        {
            ClientView view = ObjectsFactory.CreateClientView(client.Id);
            view.Name = "Name0";
            view.Nit = "Nit0";
            view.Address = "Address0";
            view.Phone = "Phone0";
            view.Contact = "Contact0";

            service.Edit(view);

            Client actual = context.Set<Client>().AsNoTracking().Single();
            Client expected = client;

            Assert.Equal(expected.BranchOfficeId, actual.BranchOfficeId);
            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Contact, actual.Contact);
            Assert.Equal(expected.Phone, actual.Phone);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Nit, actual.Nit);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region Delete(String id)

        [Fact]
        public void Delete_Client()
        {
            service.Delete(client.Id);

            Assert.Empty(context.Set<Client>());
        }

        #endregion
    }
}
