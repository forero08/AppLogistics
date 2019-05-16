using NonFactors.Mvc.Lookup;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class BranchOfficeView : BaseView
    {
        [Required]
        [StringLength(32)]
        [LookupColumn]
        public string Name { get; set; }
    }
}
