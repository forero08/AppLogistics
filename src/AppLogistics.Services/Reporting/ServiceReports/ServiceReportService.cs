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
            var services = UnitOfWork.Select<Service>()
                .Where(s => !query.ServiceId.HasValue || s.Id == query.ServiceId)
                .Where(s => !query.StartDate.HasValue || s.CreationDate >= query.StartDate)
                .Where(s => !query.EndDate.HasValue || s.CreationDate <= query.EndDate)
                .Where(s => query.ClientIds == null || query.ClientIds.Contains(s.Rate.ClientId))
                .Where(s => query.ActivityIds == null || query.ActivityIds.Contains(s.Rate.ActivityId))
                .Where(s => query.VehicleTypeIds == null || query.VehicleTypeIds.Contains(s.Rate.VehicleTypeId.Value))
                .Where(s => query.ProductIds == null || query.ProductIds.Contains(s.Rate.ProductId.Value))
                .Where(s => query.CarrierIds == null || query.CarrierIds.Contains(s.CarrierId.Value))
                .Where(s => query.SectorIds == null || query.SectorIds.Contains(s.SectorId.Value))
                .Where(s => string.IsNullOrWhiteSpace(query.VehicleNumber) || s.VehicleNumber.Contains(query.VehicleNumber))
                .Where(s => string.IsNullOrWhiteSpace(query.Location) || s.Location.Contains(query.Location))
                .Where(s => string.IsNullOrWhiteSpace(query.CustomsInformation) || s.CustomsInformation.Contains(query.CustomsInformation))
                .Where(s => string.IsNullOrWhiteSpace(query.Comments) || s.Comments.Contains(query.Comments));

            if (query.EmployeeIds?.Length > 0)
            {
                var serviceIds = UnitOfWork.Select<Holding>()
                    .Where(h => query.EmployeeIds.Contains(h.Employee.Id))
                    .Select(h => h.ServiceId);

                services = services.Where(s => serviceIds.Contains(s.Id));
            }

            return services.To<ServiceReportView>();
        }
    }
}
