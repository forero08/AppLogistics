using AppLogistics.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class ServiceReportExcelView
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        //[Display(Name = Resource.ForAction("ServiceView"))]
        public string RateClientName { get; set; }

        public string RateActivityName { get; set; }

        public string RateVehicleTypeName { get; set; }

        public string RateProductName { get; set; }

        public int Quantity { get; set; }

        public decimal ServiceFullPrice { get; set; }

        public float EmployeePercentage { get; set; }

        public decimal ServiceHoldingPrice { get; set; }

        public decimal EmployeeHoldingPrice { get; set; }

        public string EmployeeInternalCode { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeSurname { get; set; }

        public string SectorName { get; set; }

        public string CarrierName { get; set; }

        public string VehicleNumber { get; set; }

        public string Location { get; set; }

        public string CustomsInformation { get; set; }

        public string Comments { get; set; }

    }
}
