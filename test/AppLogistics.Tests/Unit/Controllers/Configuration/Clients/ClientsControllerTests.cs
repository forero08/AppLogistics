using AppLogistics.Controllers.Tests;
using AppLogistics.Objects;
using AppLogistics.Services;
using AppLogistics.Tests;
using AppLogistics.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using System.Linq;
using Xunit;

namespace AppLogistics.Controllers.Configuration.Tests
{
    public class ClientsControllerTests : ControllerTests
    {
        private ClientsController controller;
        private IClientValidator validator;
        private IClientService service;
        private ClientView client;

        public ClientsControllerTests()
        {
            validator = Substitute.For<IClientValidator>();
            service = Substitute.For<IClientService>();

            client = ObjectsFactory.CreateClientView();

            controller = Substitute.ForPartsOf<ClientsController>(validator, service);
            controller.ControllerContext.RouteData = new RouteData();
        }

        #region Index()

        [Fact]
        public void Index_ReturnsClientViews()
        {
            service.GetViews().Returns(new ClientView[0].AsQueryable());

            object actual = controller.Index().ViewData.Model;
            object expected = service.GetViews();

            Assert.Same(expected, actual);
        }

        #endregion

        #region Create()

        [Fact]
        public void Create_ReturnsEmptyView()
        {
            ViewDataDictionary actual = controller.Create().ViewData;

            Assert.Null(actual.Model);
        }

        #endregion

        #region Create(ClientView client)

        [Fact]
        public void Create_ProtectsFromOverpostingId()
        {
            ProtectsFromOverpostingId(controller, "Create");
        }

        [Fact]
        public void Create_CanNotCreate_ReturnsSameView()
        {
            validator.CanCreate(client).Returns(false);

            object actual = (controller.Create(client) as ViewResult).ViewData.Model;
            object expected = client;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Create_Client()
        {
            validator.CanCreate(client).Returns(true);

            controller.Create(client);

            service.Received().Create(client);
        }

        [Fact]
        public void Create_RedirectsToIndex()
        {
            validator.CanCreate(client).Returns(true);

            object expected = RedirectToAction(controller, "Index");
            object actual = controller.Create(client);

            Assert.Same(expected, actual);
        }

        #endregion

        #region Details(String id)

        [Fact]
        public void Details_ReturnsNotEmptyView()
        {
            service.Get<ClientView>(client.Id).Returns(client);

            object expected = NotEmptyView(controller, client);
            object actual = controller.Details(client.Id);

            Assert.Same(expected, actual);
        }

        #endregion

        #region Edit(String id)

        [Fact]
        public void Edit_ReturnsNotEmptyView()
        {
            service.Get<ClientView>(client.Id).Returns(client);

            object expected = NotEmptyView(controller, client);
            object actual = controller.Edit(client.Id);

            Assert.Same(expected, actual);
        }

        #endregion

        #region Edit(ClientView client)

        [Fact]
        public void Edit_CanNotEdit_ReturnsSameView()
        {
            validator.CanEdit(client).Returns(false);

            object actual = (controller.Edit(client) as ViewResult).ViewData.Model;
            object expected = client;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Edit_Client()
        {
            validator.CanEdit(client).Returns(true);

            controller.Edit(client);

            service.Received().Edit(client);
        }

        [Fact]
        public void Edit_RedirectsToIndex()
        {
            validator.CanEdit(client).Returns(true);

            object expected = RedirectToAction(controller, "Index");
            object actual = controller.Edit(client);

            Assert.Same(expected, actual);
        }

        #endregion

        #region Delete(String id)

        [Fact]
        public void Delete_ReturnsNotEmptyView()
        {
            service.Get<ClientView>(client.Id).Returns(client);

            object expected = NotEmptyView(controller, client);
            object actual = controller.Delete(client.Id);

            Assert.Same(expected, actual);
        }

        #endregion

        #region DeleteConfirmed(String id)

        [Fact]
        public void DeleteConfirmed_DeletesClient()
        {
            controller.DeleteConfirmed(client.Id);

            service.Received().Delete(client.Id);
        }

        [Fact]
        public void Delete_RedirectsToIndex()
        {
            object expected = RedirectToAction(controller, "Index");
            object actual = controller.DeleteConfirmed(client.Id);

            Assert.Same(expected, actual);
        }

        #endregion
    }
}
