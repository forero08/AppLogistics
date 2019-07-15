using AppLogistics.Data.Core;
using AppLogistics.Objects;
using System.Linq;

namespace AppLogistics.Services
{
    public class ServiceService : BaseService, IServiceService
    {
        public ServiceService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public TView Get<TView>(int id) where TView : BaseView
        {
            return UnitOfWork.GetAs<Service, TView>(id);
        }

        public IQueryable<ServiceView> GetViews()
        {
            return UnitOfWork
                .Select<Service>()
                .To<ServiceView>()
                .OrderByDescending(service => service.Id);
        }

        public void Create(ServiceCreateEditView view)
        {
            Service service = UnitOfWork.To<Service>(view);

            var rate = UnitOfWork.Select<Rate>().First(r => r.Id == view.RateId);
            service.FullPrice = rate.Price * view.Quantity;
            service.HoldingPrice = rate.Price * view.Quantity * (decimal)rate.EmployeePercentage / 100;

            UnitOfWork.Insert(service);
            UnitOfWork.Commit();
        }

        public void Edit(ServiceCreateEditView view)
        {
            Service service = UnitOfWork.To<Service>(view);

            var rate = UnitOfWork.Select<Rate>().First(r => r.Id == view.RateId);
            service.FullPrice = rate.Price * view.Quantity;
            service.HoldingPrice = rate.Price * view.Quantity * (decimal)rate.EmployeePercentage / 100;

            UnitOfWork.Update(service);
            UnitOfWork.Commit();
        }

        public void Delete(int id)
        {
            UnitOfWork.Delete<Service>(id);
            UnitOfWork.Commit();
        }
    }
}
