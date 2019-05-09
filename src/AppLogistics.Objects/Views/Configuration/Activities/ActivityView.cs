using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class ActivityView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
