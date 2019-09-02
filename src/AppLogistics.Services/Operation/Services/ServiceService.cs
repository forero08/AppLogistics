using AppLogistics.Data.Core;
using AppLogistics.Objects;
using System.Collections.Generic;
using System.Linq;

namespace AppLogistics.Services
{
    public class ServiceService : BaseService, IServiceService
    {
        public ServiceService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public TView Get<TView>(int id) where TView : ServiceView
        {
            var service = UnitOfWork.GetAs<Service, TView>(id);

            if (service == null)
            {
                return null;
            }

            var employees = UnitOfWork.Select<Holding>()
                .Where(h => h.ServiceId == id)
                .Select(h => h.EmployeeId)
                .ToList();

            service.SelectedEmployees = employees.ToArray();

            return service;
        }

        public TView GetEdit<TView>(int id) where TView : ServiceCreateEditView
        {
            var service = UnitOfWork.GetAs<Service, TView>(id);

            if (service == null)
            {
                return null;
            }

            var employees = UnitOfWork.Select<Holding>()
                .Where(h => h.ServiceId == id)
                .Select(h => h.EmployeeId)
                .ToList();

            service.SelectedEmployees = employees.ToArray();

            return service;
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
            List<Holding> newHoldings = GenerateHoldingsWithService(view);

            UnitOfWork.InsertRange(newHoldings);
            UnitOfWork.Commit();
        }

        private List<Holding> GenerateHoldingsWithService(ServiceCreateEditView view)
        {
            var rate = UnitOfWork.Get<Rate>(view.RateId);

            var fullPrice = rate.Price * view.Quantity;
            var holdingPrice = rate.Price * view.Quantity * (decimal)rate.EmployeePercentage / 100;
            var pricePerEmployee = rate.SplitFare ? holdingPrice / view.SelectedEmployees.Length : holdingPrice;

            var service = UnitOfWork.To<Service>(view);
            service.FullPrice = fullPrice;
            service.HoldingPrice = holdingPrice;

            var holdings = new List<Holding>();
            foreach (var employeeId in view.SelectedEmployees)
            {
                var holding = new Holding
                {
                    Employee = UnitOfWork.Get<Employee>(employeeId),
                    Price = pricePerEmployee,
                    Service = service,
                };

                holdings.Add(holding);
            }

            return holdings;
        }

        public void Edit(ServiceCreateEditView view)
        {
            var existingService = UnitOfWork.GetAsNoTracking<Service>(view.Id);

            if (RequiresNewHoldings(view, existingService))
            {
                // Delete old holdings
                var existingHoldings = UnitOfWork.Select<Holding>().Where(h => h.ServiceId == view.Id);
                UnitOfWork.DeleteRange(existingHoldings);

                // Insert new holdings
                var rate = UnitOfWork.Get<Rate>(view.RateId);
                List<Holding> holdings = GenerateUpdatedHoldings(view, rate);
                UnitOfWork.InsertRange(holdings);
                UnitOfWork.Commit();

                // Update prices
                existingService.FullPrice = rate.Price * view.Quantity;
                existingService.HoldingPrice = rate.Price * view.Quantity * (decimal)rate.EmployeePercentage / 100;
            }

            var updatedService = UnitOfWork.To<Service>(view);
            updatedService.FullPrice = existingService.FullPrice;
            updatedService.HoldingPrice = existingService.HoldingPrice;

            UnitOfWork.Update(updatedService);
            UnitOfWork.Commit();
        }

        private bool RequiresNewHoldings(ServiceCreateEditView view, Service existingService)
        {
            if (view.Quantity != existingService.Quantity || view.RateId != existingService.RateId)
            {
                return true;
            }

            var employeeIds = UnitOfWork.Select<Holding>()
                .Where(h => h.ServiceId == view.Id)
                .Select(h => h.EmployeeId)
                .ToList()
                .OrderBy(e => e);

            return employeeIds.SequenceEqual(view.SelectedEmployees.OrderBy(e => e)) ? false : true;
        }

        private List<Holding> GenerateUpdatedHoldings(ServiceCreateEditView view, Rate rate)
        {
            var holdingPrice = rate.Price * view.Quantity * (decimal)rate.EmployeePercentage / 100;
            var pricePerEmployee = rate.SplitFare ? holdingPrice / view.SelectedEmployees.Length : holdingPrice;

            var holdings = new List<Holding>();
            foreach (var employeeId in view.SelectedEmployees)
            {
                var holding = new Holding
                {
                    EmployeeId = employeeId,
                    Price = pricePerEmployee,
                    ServiceId = view.Id,
                };

                holdings.Add(holding);
            }

            return holdings;
        }

        public void Delete(int id)
        {
            var existingHoldings = UnitOfWork.Select<Holding>().Where(h => h.ServiceId == id);
            UnitOfWork.DeleteRange(existingHoldings);

            UnitOfWork.Delete<Service>(id);
            UnitOfWork.Commit();
        }
    }
}
