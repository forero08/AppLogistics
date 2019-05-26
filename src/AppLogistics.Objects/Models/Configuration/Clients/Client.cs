﻿using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class Client : BaseModel
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(16)]
        public string Nit { get; set; }

        [Required]
        [StringLength(64)]
        public string Address { get; set; }

        [StringLength(16)]
        [Phone]
        public string Phone { get; set; }

        [StringLength(32)]
        public string Contact { get; set; }

        public int BranchOfficeId { get; set; }
        public virtual BranchOffice BranchOffice { get; set; }
    }
}
