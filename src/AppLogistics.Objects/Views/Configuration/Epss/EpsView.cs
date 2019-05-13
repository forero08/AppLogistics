using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class EpsView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(16)]
        public string Nit { get; set; }
    }
}
