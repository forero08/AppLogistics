using AppLogistics.Components.ExcelReports;
using AppLogistics.Data.Core;
using AppLogistics.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLogistics.Services
{
    public class ServiceReportService : BaseService, IServiceReportService
    {
        private readonly IExcelReportCreator _excelReportCreator;

        public ServiceReportService(IUnitOfWork unitOfWork, IExcelReportCreator excelReportCreator)
            : base(unitOfWork)
        {
            _excelReportCreator = excelReportCreator;
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
            var mappedServices = GetExcelFilteredByQuery(query);
            return _excelReportCreator.CreateServiceReport(mappedServices);
        }

        private IList<ServiceReportExcelView> GetExcelFilteredByQuery(ServiceReportQueryView query)
        {
            var filteredServices = FilterServices(query);

            var reportRows = filteredServices.Select(s => new ServiceReportExcelView
            {
                ActivityName = s.Rate.Activity.Name,
                CarrierName = s.Carrier.Name,
                ClientName = s.Rate.Client.Name,
                Comments = s.Comments,
                CreationDate = s.CreationDate,
                CreationTime = s.CreationDate,
                CustomsInformation = s.CustomsInformation,
                EmployeePercentage = s.Rate.EmployeePercentage,
                Location = s.Location,
                Quantity = s.Quantity,
                ProductName = s.Rate.Product.Name,
                RatePrice = s.Rate.Price,
                RateSplitFare = s.Rate.SplitFare,
                SectorName = s.Sector.Name,
                ServiceFullPrice = s.FullPrice,
                ServiceHoldingPrice = s.HoldingPrice,
                ServiceId = s.Id,
                VehicleNumber = s.VehicleNumber,
                VehicleTypeName = s.Rate.VehicleType.Name
            })
            .Distinct()
            .OrderBy(s => s.ServiceId)
            .ToList();

            foreach (var row in reportRows)
            {
                var holdingXemployees = UnitOfWork.Select<Holding>()
                    .Join(UnitOfWork.Select<Employee>(), hold => hold.EmployeeId, emp => emp.Id, (hold, emp) => new { hold, emp });

                row.EmployeesInfo = holdingXemployees.Where(x => x.hold.ServiceId == row.ServiceId)
                    .Select(empRep => new ServiceReportEmployeeExcelView
                    {
                        EmployeeName = empRep.emp.Name,
                        EmployeeInternalCode = empRep.emp.InternalCode,
                        EmployeeHoldingPrice = empRep.hold.Price
                    });
                row.EmployeesQuantity = row.EmployeesInfo.Count();
            }

            return reportRows;
        }
    }
}
