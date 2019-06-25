using System;

namespace AppLogistics.Objects
{
    public class EmployeeView : BaseView
    {
        public string InternalCode { get; set; }

        public string DocumentTypeName { get; set; }

        public string DocumentNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BornDate { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? RetirementDate { get; set; }

        public string ResidenceCity { get; set; }

        public string Address { get; set; }

        public string CountryName { get; set; }

        public string MobilePhone { get; set; }

        public string HomePhone { get; set; }

        public string Email { get; set; }

        public string MaritalStatusName { get; set; }

        public string EthnicGroupName { get; set; }

        public string EducationLevelName { get; set; }

        public string EmergencyContact { get; set; }

        public string EmergencyContactPhone { get; set; }

        public string AfpName { get; set; }

        public string EpsName { get; set; }

        public bool HasCurriculumVitae { get; set; }

        public bool HasDocumentCopy { get; set; }

        public bool HasPhotos { get; set; }

        public bool HasMilitaryIdCopy { get; set; }

        public bool HasLaborCertification { get; set; }

        public bool HasPersonalReference { get; set; }

        public bool HasDisciplinaryBackground { get; set; }

        public bool HasKnowledgeTest { get; set; }

        public bool HasAdmissionTest { get; set; }

        public bool HasContract { get; set; }

        public bool HasInternalRegulations { get; set; }

        public bool HasEndownmentLetter { get; set; }

        public bool IsCriticalPosition { get; set; }

        public string Comments { get; set; }
    }
}
