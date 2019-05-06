using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class EpsView : BaseView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Nit { get; set; }
    }
}
