using NonFactors.Mvc.Lookup;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class SectorView : BaseView
    {
        [Required]
        [LookupColumn]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
