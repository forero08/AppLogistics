using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class VehicleTypeView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
