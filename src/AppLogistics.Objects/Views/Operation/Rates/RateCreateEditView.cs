using AppLogistics.Components.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class RateCreateEditView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        public int? VehicleTypeId { get; set; }

        public int? ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxValue(100)]
        public float EmployeePercentage { get; set; }

        [Required]
        public bool SplitFare { get; set; }
    }
}
