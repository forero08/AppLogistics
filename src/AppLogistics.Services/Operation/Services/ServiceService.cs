using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Objects.Models.Operation.Services;
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

            var prices = CalculateServicePrices(rate, view);

            var service = UnitOfWork.To<Service>(view);
            service.FullPrice = prices.FullPrice;
            service.HoldingPrice = prices.HoldingPrice;

            var holdings = new List<Holding>();
            foreach (var employeeId in view.SelectedEmployees)
            {
                var holding = new Holding
                {
                    Employee = UnitOfWork.Get<Employee>(employeeId),
                    Price = prices.PricePerEmployee,
                    Service = service,
                };

                holdings.Add(holding);
            }

            return holdings;
        }

        private ServicePrices CalculateServicePrices(Rate rate, ServiceCreateEditView view)
        {
            var fullPrice = rate.Price * view.Quantity * view.SelectedEmployees.Length;
            var holdingPrice = fullPrice * (decimal)rate.EmployeePercentage / 100;

            if (rate.SplitFare)
            {
                fullPrice = fullPrice / view.SelectedEmployees.Length;
                holdingPrice = fullPrice * (decimal)rate.EmployeePercentage / 100;
            }

            var pricePerEmployee = holdingPrice / view.SelectedEmployees.Length;

            return new ServicePrices
            {
                FullPrice = fullPrice,
                HoldingPrice = holdingPrice,
                PricePerEmployee = pricePerEmployee
            };
        }

        public void Edit(ServiceCreateEditView view)
        {
            var existingService = UnitOfWork.GetAsNoTracking<Service>(view.Id);
            var rate = UnitOfWork.Get<Rate>(view.RateId);
            var prices = CalculateServicePrices(rate, view);

            // Delete old holdings
            var existingHoldings = UnitOfWork.Select<Holding>().Where(h => h.ServiceId == view.Id);
            UnitOfWork.DeleteRange(existingHoldings);

            // Insert new holdings
            List<Holding> holdings = GenerateUpdatedHoldings(view, prices.PricePerEmployee);
            UnitOfWork.InsertRange(holdings);
            UnitOfWork.Commit();

            var updatedService = UnitOfWork.To<Service>(view);
            updatedService.FullPrice = prices.FullPrice;
            updatedService.HoldingPrice = prices.HoldingPrice;

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

        private List<Holding> GenerateUpdatedHoldings(ServiceCreateEditView view, decimal pricePerEmployee)
        {
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
