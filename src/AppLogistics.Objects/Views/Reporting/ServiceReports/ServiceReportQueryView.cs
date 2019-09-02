using System;

namespace AppLogistics.Objects
{
    public class ServiceReportQueryView
    {
        public int? ServiceId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ClientId { get; set; }

        public int? ActivityId { get; set; }

        public string EmployeeInternalCode { get; set; }
    }
}
