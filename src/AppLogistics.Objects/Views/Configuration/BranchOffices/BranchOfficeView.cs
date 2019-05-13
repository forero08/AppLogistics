using System;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class BranchOfficeView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
