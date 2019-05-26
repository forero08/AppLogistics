using AppLogistics.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppLogistics.Objects
{
    public class Employee : BaseModel
    {
        [Required]
        [StringLength(16)]
        [Index(IsUnique = true)]
        public string InternalCode { get; set; }

        public int BranchOfficeId { get; set; }
        public virtual BranchOffice BranchOffice { get; set; }

        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }

        [Required]
        [StringLength(16)]
        public string DocumentNumber { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Surname { get; set; }

        [Required]
        public DateTime BornDate { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public DateTime? RetirementDate { get; set; }

        [Required]
        [StringLength(32)]
        public string ResidenceCity { get; set; }

        [Required]
        [StringLength(128)]
        public string Address { get; set; }

        [Phone]
        [Required]
        [StringLength(16)]
        public string MobilePhone { get; set; }

        [Phone]
        [StringLength(16)]
        public string HomePhone { get; set; }

        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }

        public int MaritalStatusId { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }

        [StringLength(32)]
        public string EmergencyContact { get; set; }

        [Phone]
        [StringLength(16)]
        public string EmergencyContactPhone { get; set; }

        public int AfpId { get; set; }
        public virtual Afp Afp { get; set; }

        public int EpsId { get; set; }
        public virtual Eps Eps { get; set; }

        [Required]
        public bool HasCurriculumVitae { get; set; }

        [Required]
        public bool HasDocumentCopy { get; set; }

        [Required]
        public bool HasPhotos { get; set; }

        [Required]
        public bool HasMilitaryIdCopy { get; set; }

        [Required]
        public bool HasLaborCertification { get; set; }

        [Required]
        public bool HasPersonalReference { get; set; }

        [Required]
        public bool HasDisciplinaryBackground { get; set; }

        [Required]
        public bool HasKnowledgeTest { get; set; }

        [Required]
        public bool HasAdmissionTest { get; set; }

        [Required]
        public bool HasContract { get; set; }

        [Required]
        public bool HasInternalRegulations { get; set; }

        [Required]
        public bool HasEndownmentLetter { get; set; }

        [Required]
        public bool IsCriticalPosition { get; set; }

        [StringLength(512)]
        public string Comments { get; set; }
    }
}
