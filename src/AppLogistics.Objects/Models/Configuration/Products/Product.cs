﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
