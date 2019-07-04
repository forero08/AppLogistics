using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppLogistics.Objects.Models.Operation.Services
{
    public class Service : BaseModel
    {
        [Required]
        public DateTime ExecutionDate { get; set; }

        public int RateId { get; set; }
        public virtual Rate Rate { get; set; }

        public int Quantity { get; set; }

        // product and product quantity?

        [StringLength(16)]
        public string VehicleNumber { get; set; }

        [Required]
        [StringLength(32)]
        public string Location { get; set; }

        [StringLength(32)]
        public string CustomsInformation { get; set; }

        public decimal FullPrice { get; set; }

        public decimal HoldingPrice { get; set; }

        [StringLength(32)]
        public string Comments { get; set; }
    }
}
