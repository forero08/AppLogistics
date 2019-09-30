using AppLogistics.Data.Core;
using AppLogistics.Objects;
using OfficeOpenXml;
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
            IQuery<Service> services = FilterServices(query);

            return services.To<ServiceReportView>();
        }

        private IQuery<Service> FilterServices(ServiceReportQueryView query)
        {
            var services = UnitOfWork.Select<Service>()
                            .Where(s => !query.ServiceId.HasValue || s.Id == query.ServiceId.Value)
                            .Where(s => !query.StartDate.HasValue || s.CreationDate >= query.StartDate.Value)
                            .Where(s => !query.EndDate.HasValue || s.CreationDate < query.EndDate.Value.AddDays(1))
                            .Where(s => query.ClientIds == null || query.ClientIds.Contains(s.Rate.ClientId.ToString()))
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

            return services;
        }

        public byte[] GetExcelReport(ServiceReportQueryView query)
        {
            IOrderedQueryable<ServiceReportExcelView> mappedServices = GetExcelFilteredByQuery(query);
            return CreateExcelReport(mappedServices);
        }

        private static byte[] CreateExcelReport(IOrderedQueryable<ServiceReportExcelView> mappedServices)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Report");

                worksheet.Cells["A1"].LoadFromCollection(mappedServices, true);
                worksheet.Column(2).Style.Numberformat.Format = "yyyy-MM-dd hh:mm";
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return excelPackage.GetAsByteArray();
            }
        }

        private IOrderedQueryable<ServiceReportExcelView> GetExcelFilteredByQuery(ServiceReportQueryView query)
        {
            var filteredServices = FilterServices(query);

            var holdings = UnitOfWork.Select<Holding>();
            var employees = UnitOfWork.Select<Employee>();

            return filteredServices.Join(holdings, s => s.Id, h => h.ServiceId, (service, holding) => new { service, holding })
                .Join(employees, x => x.holding.EmployeeId, e => e.Id, (serviceXholding, employee) => new { serviceXholding, employee })
                .Select(csv => new ServiceReportExcelView
                {
                    Id = csv.serviceXholding.service.Id,
                    CreationDate = csv.serviceXholding.service.CreationDate,
                    RateClientName = csv.serviceXholding.service.Rate.Client.Name,
                    RateActivityName = csv.serviceXholding.service.Rate.Activity.Name,
                    Quantity = csv.serviceXholding.service.Quantity,
                    ServiceFullPrice = csv.serviceXholding.service.FullPrice,
                    EmployeeInternalCode = csv.employee.InternalCode,
                    EmployeeName = csv.employee.Name,
                    EmployeeHoldingPrice = csv.serviceXholding.holding.Price,
                })
                .OrderBy(service => service.Id)
                .ThenBy(service => service.EmployeeInternalCode);
        }
    }
}
