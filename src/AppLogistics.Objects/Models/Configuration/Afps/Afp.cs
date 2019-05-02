﻿using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class Afp : BaseModel
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(16)]
        public string Nit { get; set; }
    }
}
