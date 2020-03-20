using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Resources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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
            var mappedServices = GetExcelFilteredByQuery(query);
            return CreateExcelReport(mappedServices);
        }

        private byte[] CreateExcelReport(IEnumerable<ServiceReportExcelView> mappedServices)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Report");

                WriteTittles(worksheet, mappedServices);

                WriteInfo(worksheet, mappedServices);

                FormatSheet(worksheet);

                return excelPackage.GetAsByteArray();
            }
        }

        private void WriteTittles(ExcelWorksheet worksheet, IEnumerable<ServiceReportExcelView> mappedServices)
        {
            worksheet.Cells[1, 1].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.ServiceId));
            worksheet.Cells[1, 2].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.CreationDate));
            worksheet.Cells[1, 3].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.CreationTime));
            worksheet.Cells[1, 4].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.ClientName));
            worksheet.Cells[1, 5].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.CarrierName));
            worksheet.Cells[1, 6].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.ActivityName));
            worksheet.Cells[1, 7].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.VehicleTypeName));
            worksheet.Cells[1, 8].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.VehicleNumber));
            worksheet.Cells[1, 9].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.ProductName));
            worksheet.Cells[1, 10].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.Quantity));
            worksheet.Cells[1, 11].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.ServiceFullPrice));
            worksheet.Cells[1, 12].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.EmployeePercentage));
            worksheet.Cells[1, 13].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.ServiceHoldingPrice));
            worksheet.Cells[1, 14].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.SectorName));
            worksheet.Cells[1, 15].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.CustomsInformation));
            worksheet.Cells[1, 16].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.Location));
            worksheet.Cells[1, 17].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportExcelView.Comments));
            worksheet.Cells[1, 18].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportEmployeeExcelView.EmployeeId));
            worksheet.Cells[1, 19].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportEmployeeExcelView.EmployeeInternalCode));
            worksheet.Cells[1, 20].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportEmployeeExcelView.EmployeeName));
            worksheet.Cells[1, 21].Value = GetMessageFromResource("ExcelServiceReport", nameof(ServiceReportEmployeeExcelView.EmployeeHoldingPrice));
        }

        private string GetMessageFromResource(string reportName, string attribute)
        {
            return Resource.ForProperty(reportName, attribute);
        }

        private void WriteInfo(ExcelWorksheet worksheet, IEnumerable<ServiceReportExcelView> mappedServices)
        {
            var rowNumber = 2;

            foreach (var serviceRow in mappedServices)
            {
                worksheet.Cells[rowNumber, 1].Value = serviceRow.ServiceId;
                worksheet.Cells[rowNumber, 2].Value = serviceRow.CreationDate;
                worksheet.Cells[rowNumber, 3].Value = serviceRow.CreationTime;
                worksheet.Cells[rowNumber, 4].Value = serviceRow.ClientName;
                worksheet.Cells[rowNumber, 5].Value = serviceRow.CarrierName;
                worksheet.Cells[rowNumber, 6].Value = serviceRow.ActivityName;
                worksheet.Cells[rowNumber, 7].Value = serviceRow.VehicleTypeName;
                worksheet.Cells[rowNumber, 8].Value = serviceRow.VehicleNumber;
                worksheet.Cells[rowNumber, 9].Value = serviceRow.ProductName;
                worksheet.Cells[rowNumber, 10].Value = serviceRow.Quantity;
                worksheet.Cells[rowNumber, 11].Value = serviceRow.ServiceFullPrice;
                worksheet.Cells[rowNumber, 12].Value = serviceRow.EmployeePercentage;
                worksheet.Cells[rowNumber, 13].Value = serviceRow.ServiceHoldingPrice;
                worksheet.Cells[rowNumber, 14].Value = serviceRow.SectorName;
                worksheet.Cells[rowNumber, 15].Value = serviceRow.CustomsInformation;
                worksheet.Cells[rowNumber, 16].Value = serviceRow.Location;
                worksheet.Cells[rowNumber, 17].Value = serviceRow.Comments;

                foreach (var employeeData in serviceRow.EmployeesInfo)
                {
                    worksheet.Cells[rowNumber, 18].Value = employeeData.EmployeeId;
                    worksheet.Cells[rowNumber, 19].Value = employeeData.EmployeeInternalCode;
                    worksheet.Cells[rowNumber, 20].Value = employeeData.EmployeeName;
                    worksheet.Cells[rowNumber, 21].Value = employeeData.EmployeeHoldingPrice;
                    rowNumber++;
                }
            }
        }

        private void FormatSheet(ExcelWorksheet worksheet)
        {
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
            worksheet.Column(21).Style.Numberformat.Format = "$ #,##0.00";

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Cells[1, 1, 1, 20].AutoFilter = true;
            worksheet.View.ZoomScale = 90;

            worksheet.Calculate();
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }

        private IEnumerable<ServiceReportExcelView> GetExcelFilteredByQuery(ServiceReportQueryView query)
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
                        EmployeeId = empRep.emp.Id,
                        EmployeeName = empRep.emp.Name,
                        EmployeeInternalCode = empRep.emp.InternalCode,
                        EmployeeHoldingPrice = empRep.hold.Price
                    });
            }

            return reportRows;
        }
    }
}
