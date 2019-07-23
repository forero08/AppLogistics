using AppLogistics.Components.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class ServiceCreateEditView : BaseView
    {
        [Required]
        public int RateClientId { get; set; }

        [Required]
        public int RateId { get; set; }

        [MinValue(1)]
        public int Quantity { get; set; }

        public int? SectorId { get; set; }

        public int? CarrierId { get; set; }

        [StringLength(16)]
        public string VehicleNumber { get; set; }

        [Required]
        [StringLength(32)]
        public string Location { get; set; }

        [StringLength(32)]
        public string CustomsInformation { get; set; }

        [StringLength(128)]
        public string Comments { get; set; }
    }
}
