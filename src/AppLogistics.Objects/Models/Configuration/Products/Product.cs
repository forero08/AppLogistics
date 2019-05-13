using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
