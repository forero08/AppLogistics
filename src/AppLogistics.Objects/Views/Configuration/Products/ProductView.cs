using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class ProductView : BaseView
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
