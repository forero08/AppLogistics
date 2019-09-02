using AppLogistics.Data.Core;
using AppLogistics.Objects;
using System.Linq;

namespace AppLogistics.Services
{
    public class ServiceReportService : BaseService, IServiceReportService
    {
        public ServiceReportService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ServiceReportView GetDetail(int id)
        {
            var serviceDetail = UnitOfWork.GetAs<Service, ServiceReportView>(id);

            if (serviceDetail == null)
            {
                return null;
            }

            var employees = UnitOfWork.Select<Holding>()
                .Where(h => h.ServiceId == id)
                .Select(h => h.EmployeeId)
                .ToList();

            serviceDetail.SelectedEmployees = employees.ToArray();

            return serviceDetail;
        }

        public IQueryable<ServiceReportView> FilterByQuery(ServiceReportQueryView query)
        {
            var services = UnitOfWork.Select<Service>();

            if (query.ServiceId.HasValue)
            {
                services = services.Where(s => s.Id == query.ServiceId);
            }

            if (query.StartDate.HasValue)
            {
                services = services.Where(s => s.CreationDate >= query.StartDate);
            }

            if (query.EndDate.HasValue)
            {
                services = services.Where(s => s.CreationDate <= query.EndDate);
            }

            if (query.ClientId.HasValue)
            {
                services = services.Where(s => s.Rate.ClientId == query.ClientId);
            }

            if (query.ActivityId.HasValue)
            {
                services = services.Where(s => s.Rate.ActivityId == query.ActivityId);
            }

            if (!string.IsNullOrWhiteSpace(query.EmployeeInternalCode))
            {
                var serviceIds = UnitOfWork.Select<Holding>()
                    .Where(h => h.Employee.InternalCode.Equals(query.EmployeeInternalCode))
                    .Select(h => h.ServiceId);

                services = services.Where(s => serviceIds.Contains(s.Id));
            }

            return services.To<ServiceReportView>();
        }
    }
}
