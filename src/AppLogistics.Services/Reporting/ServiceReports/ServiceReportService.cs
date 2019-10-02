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

                worksheet.Cells[1, 1].LoadFromCollection(mappedServices, true);

                // CreationDate
                worksheet.Column(2).Style.Numberformat.Format = "yyyy-MM-dd";
                // CreationTime
                worksheet.Column(3).Style.Numberformat.Format = "hh:mm";
                // ServiceFullPrice
                worksheet.Column(11).Style.Numberformat.Format = "$ #,##0.00";
                // EmployeePercentage
                worksheet.Column(12).Style.Numberformat.Format = "#0\\%";
                // ServiceHoldingPrice
                worksheet.Column(13).Style.Numberformat.Format = "$ #,##0.00";
                // EmployeeHoldingPrice
                worksheet.Column(14).Style.Numberformat.Format = "$ #,##0.00";

                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Cells[1, 1, 1, 20].AutoFilter = true;
                worksheet.View.ZoomScale = 90;

                worksheet.Calculate();
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return excelPackage.GetAsByteArray();
            }
        }

        private IOrderedQueryable<ServiceReportExcelView> GetExcelFilteredByQuery(ServiceReportQueryView query)
        {
            var filteredServices = FilterServices(query);

            var holdings = UnitOfWork.Select<Holding>();
            var employees = UnitOfWork.Select<Employee>();

            return filteredServices.Join(holdings, s => s.Id, h => h.ServiceId, (Service, Holding) => new { Service, Holding })
                .Join(employees, x => x.Holding.EmployeeId, e => e.Id, (serviceXholding, Employee) => new { serviceXholding, Employee })
                .Select(info => new ServiceReportExcelView
                {
                    ServiceId = info.serviceXholding.Service.Id,
                    CreationDate = info.serviceXholding.Service.CreationDate,
                    CreationTime = info.serviceXholding.Service.CreationDate,
                    ClientName = info.serviceXholding.Service.Rate.Client.Name,
                    ActivityName = info.serviceXholding.Service.Rate.Activity.Name,
                    VehicleTypeName = info.serviceXholding.Service.Rate.VehicleType.Name,
                    ProductName = info.serviceXholding.Service.Rate.Product.Name,
                    Quantity = info.serviceXholding.Service.Quantity,
                    ServiceFullPrice = info.serviceXholding.Service.FullPrice,
                    EmployeePercentage = info.serviceXholding.Service.Rate.EmployeePercentage,
                    ServiceHoldingPrice = info.serviceXholding.Service.HoldingPrice,
                    EmployeeInternalCode = info.Employee.InternalCode,
                    EmployeeName = $"{info.Employee.Name} {info.Employee.Surname}",
                    EmployeeHoldingPrice = info.serviceXholding.Holding.Price,
                    CarrierName = info.serviceXholding.Service.Carrier.Name,
                    VehicleNumber = info.serviceXholding.Service.VehicleNumber,
                    SectorName = info.serviceXholding.Service.Sector.Name,
                    CustomsInformation = info.serviceXholding.Service.CustomsInformation,
                    Location = info.serviceXholding.Service.Location,
                    Comments = info.serviceXholding.Service.Comments
                })
                .OrderBy(service => service.CreationDate)
                .ThenBy(service => service.EmployeeInternalCode);
        }
    }
}
