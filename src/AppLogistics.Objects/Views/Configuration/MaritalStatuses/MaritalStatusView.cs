using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class MaritalStatusView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
