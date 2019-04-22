using AppLogistics.Components.Extensions;
using NonFactors.Mvc.Lookup;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class RoleView : BaseView
    {
        [Required]
        [LookupColumn]
        [StringLength(128)]
        public string Title { get; set; }

        public MvcTree Permissions { get; set; }

        public RoleView()
        {
            Permissions = new MvcTree();
        }
    }
}
