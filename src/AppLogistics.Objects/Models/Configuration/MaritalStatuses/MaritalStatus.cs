﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class MaritalStatus : BaseModel
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
