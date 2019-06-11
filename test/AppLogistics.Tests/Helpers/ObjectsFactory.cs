using AppLogistics.Objects;
using System;
using System.Collections.Generic;

namespace AppLogistics.Tests
{
    public static class ObjectsFactory
    {
        #region Administration

        public static Account CreateAccount(int id = 0)
        {
            return new Account
            {
                Username = "Username" + id,
                Passhash = "Passhash" + id,

                Email = id + "@tests.com",

                IsLocked = false,

                RecoveryToken = "Token" + id,
                RecoveryTokenExpirationDate = DateTime.Now.AddMinutes(5),

                Role = CreateRole(id)
            };
        }

        public static AccountView CreateAccountView(int id = 0)
        {
            return new AccountView
            {
                Id = id,

                Username = "Username" + id,
                Email = id + "@tests.com",

                IsLocked = true,

                RoleTitle = "Title" + id
            };
        }

        public static AccountEditView CreateAccountEditView(int id = 0)
        {
            return new AccountEditView
            {
                Id = id,

                Username = "Username" + id,
                Email = id + "@tests.com",

                IsLocked = true,

                RoleId = id
            };
        }

        public static AccountCreateView CreateAccountCreateView(int id = 0)
        {
            return new AccountCreateView
            {
                Username = "Username" + id,
                Password = "Password" + id,

                Email = id + "@tests.com",

                RoleId = id
            };
        }

        public static AccountLoginView CreateAccountLoginView(int id = 0)
        {
            return new AccountLoginView
            {
                Username = "Username" + id,
                Password = "Password" + id
            };
        }

        public static AccountResetView CreateAccountResetView(int id = 0)
        {
            return new AccountResetView
            {
                Token = "Token" + id,
                NewPassword = "NewPassword" + id
            };
        }

        public static AccountRecoveryView CreateAccountRecoveryView(int id = 0)
        {
            return new AccountRecoveryView
            {
                Email = id + "@tests.com"
            };
        }

        public static ProfileEditView CreateProfileEditView(int id = 0)
        {
            return new ProfileEditView
            {
                Id = id,

                Email = id + "@tests.com",
                Username = "Username" + id,

                Password = "Password" + id,
                NewPassword = "NewPassword" + id
            };
        }

        public static ProfileDeleteView CreateProfileDeleteView(int id = 0)
        {
            return new ProfileDeleteView
            {
                Id = id,

                Password = "Password" + id
            };
        }

        public static Role CreateRole(int id = 0)
        {
            return new Role
            {
                Title = "Title" + id,

                Accounts = new List<Account>(),
                Permissions = new List<RolePermission>()
            };
        }

        public static RoleView CreateRoleView(int id = 0)
        {
            return new RoleView
            {
                Id = id,

                Title = "Title" + id
            };
        }

        public static Permission CreatePermission(int id = 0)
        {
            return new Permission
            {
                Id = id,

                Area = "Area" + id,
                Action = "Action" + id,
                Controller = "Controller" + id
            };
        }

        public static RolePermission CreateRolePermission(int id = 0)
        {
            return new RolePermission
            {
                RoleId = id,
                Role = CreateRole(id),

                PermissionId = id,
                Permission = CreatePermission(id)
            };
        }

        #endregion Administration

        #region Configuration

        public static Activity CreateActivity(int id = 0)
        {
            return new Activity
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static ActivityView CreateActivityView(int id = 0)
        {
            return new ActivityView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static Afp CreateAfp(int id = 0)
        {
            return new Afp
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id
            };
        }

        public static AfpView CreateAfpView(int id = 0)
        {
            return new AfpView
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id
            };
        }

        public static BranchOffice CreateBranchOffice(int id = 0)
        {
            return new BranchOffice
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static BranchOfficeView CreateBranchOfficeView(int id = 0)
        {
            return new BranchOfficeView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static Carrier CreateCarrier(int id = 0)
        {
            return new Carrier
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id
            };
        }

        public static CarrierView CreateCarrierView(int id = 0)
        {
            return new CarrierView
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id
            };
        }

        public static Client CreateClient(int id = 0)
        {
            return new Client
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id,
                Address = "Address" + id,
                Phone = "Phone" + id,
                Contact = "Contact" + id,
                BranchOffice = CreateBranchOffice(id)
            };
        }

        public static ClientView CreateClientView(int id = 0)
        {
            return new ClientView
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id,
                Address = "Address" + id,
                Phone = "Phone" + id,
                Contact = "Contact" + id
            };
        }

        public static ClientCreateEditView CreateClientCreateEditView(int id = 0)
        {
            return new ClientCreateEditView
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id,
                Address = "Address" + id,
                Phone = "Phone" + id,
                Contact = "Contact" + id
            };
        }

        public static DocumentType CreateDocumentType(int id = 0)
        {
            return new DocumentType
            {
                Id = id,
                Name = "Name" + id,
                ShortName = "ShortName" + id
            };
        }

        public static DocumentTypeView CreateDocumentTypeView(int id = 0)
        {
            return new DocumentTypeView
            {
                Id = id,
                Name = "Name" + id,
                ShortName = "ShortName" + id
            };
        }

        public static EducationLevel CreateEducationLevel(int id = 0)
        {
            return new EducationLevel
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static EducationLevelView CreateEducationLevelView(int id = 0)
        {
            return new EducationLevelView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static Eps CreateEps(int id = 0)
        {
            return new Eps
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id
            };
        }

        public static EpsView CreateEpsView(int id = 0)
        {
            return new EpsView
            {
                Id = id,
                Name = "Name" + id,
                Nit = "Nit" + id
            };
        }

        public static EthnicGroup CreateEthnicGroup(int id = 0)
        {
            return new EthnicGroup
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static EthnicGroupView CreateEthnicGroupView(int id = 0)
        {
            return new EthnicGroupView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static MaritalStatus CreateMaritalStatus(int id = 0)
        {
            return new MaritalStatus
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static MaritalStatusView CreateMaritalStatusView(int id = 0)
        {
            return new MaritalStatusView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static Product CreateProduct(int id = 0)
        {
            return new Product
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static ProductView CreateProductView(int id = 0)
        {
            return new ProductView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static VehicleType CreateVehicleType(int id = 0)
        {
            return new VehicleType
            {
                Id = id,
                Name = "Name" + id
            };
        }

        public static VehicleTypeView CreateVehicleTypeView(int id = 0)
        {
            return new VehicleTypeView
            {
                Id = id,
                Name = "Name" + id
            };
        }

        #endregion Configuration

        #region Operation

        public static Employee CreateEmployee(int id = 0)
        {
            return new Employee
            {
                Id = id,
                Name = "Name" + id,
                Address = "Address" + id,
                BornDate = DateTime.Today,
                Comments = "Comments" + id,
                DocumentNumber = "DocumentNumber" + id,
                Email = "Email" + id,
                EmergencyContact = "EmergencyContact" + id,
                EmergencyContactPhone = "EmergencyContactPhone" + id,
                HasAdmissionTest = true,
                HasContract = true,
                HasCurriculumVitae = true,
                HasDisciplinaryBackground = true,
                HasDocumentCopy = true,
                HasEndownmentLetter = true,
                HasInternalRegulations = true,
                HasKnowledgeTest = true,
                HasLaborCertification = true,
                HasMilitaryIdCopy = true,
                HasPersonalReference = true,
                HasPhotos = true,
                HireDate = DateTime.Today,
                HomePhone = "HomePhone" + id,
                InternalCode = "InternalCode" + id,
                IsCriticalPosition = true,
                MobilePhone = "MobilePhone" + id,
                ResidenceCity = "ResidenceCity" + id,
                RetirementDate = DateTime.Today,
                Surname = "Surname" + id,

                Afp = CreateAfp(id),
                BranchOffice = CreateBranchOffice(id),
                DocumentType = CreateDocumentType(id),
                EducationLevel = CreateEducationLevel(id),
                Eps = CreateEps(id),
                EthnicGroup = CreateEthnicGroup(id),
                MaritalStatus = CreateMaritalStatus(id)
            };
        }

        public static EmployeeView CreateEmployeeView(int id = 0)
        {
            return new EmployeeView
            {
                Id = id,
                Name = "Name" + id,
                Address = "Address" + id,
                AfpName = "AfpName" + id,
                BornDate = DateTime.Now,
                BranchOfficeName = "BranchOfficeName" + id,
                Comments = "Comments" + id,
                DocumentNumber = "DocumentNumber" + id,
                DocumentTypeName = "DocumentTypeName" + id,
                Email = "Email" + id,
                EmergencyContact = "EmergencyContact" + id,
                EmergencyContactPhone = "EmergencyContactPhone" + id,
                EpsName = "EpsName" + id,
                EthnicGroupName = "EthnicGroupName" + id,
                HasAdmissionTest = true,
                HasContract = true,
                HasCurriculumVitae = true,
                HasDisciplinaryBackground = true,
                HasDocumentCopy = true,
                HasEndownmentLetter = true,
                HasInternalRegulations = true,
                HasKnowledgeTest = true,
                HasLaborCertification = true,
                HasMilitaryIdCopy = true,
                HasPersonalReference = true,
                HasPhotos = true,
                HireDate = DateTime.Now,
                HomePhone = "HomePhone" + id,
                InternalCode = "InternalCode" + id,
                IsCriticalPosition = true,
                MaritalStatusName = "MaritalStatusName" + id,
                MobilePhone = "MobilePhone" + id,
                ResidenceCity = "ResidenceCity" + id,
                RetirementDate = DateTime.Now,
                Surname = "Surname" + id
            };
        }

        public static EmployeeCreateEditView CreateEmployeeCreateEditView(int id = 0)
        {
            return new EmployeeCreateEditView
            {
                Id = id,
                Name = "Name" + id,
                Address = "Address" + id,
                AfpId = id,
                BornDate = DateTime.Now,
                BranchOfficeId = id,
                Comments = "Comments" + id,
                DocumentNumber = "DocumentNumber" + id,
                DocumentTypeId = id,
                Email = "Email" + id,
                EmergencyContact = "EmergencyContact" + id,
                EmergencyContactPhone = "EmergencyContactPhone" + id,
                EpsId = id,
                EthnicGroupId = id,
                HasAdmissionTest = true,
                HasContract = true,
                HasCurriculumVitae = true,
                HasDisciplinaryBackground = true,
                HasDocumentCopy = true,
                HasEndownmentLetter = true,
                HasInternalRegulations = true,
                HasKnowledgeTest = true,
                HasLaborCertification = true,
                HasMilitaryIdCopy = true,
                HasPersonalReference = true,
                HasPhotos = true,
                HireDate = DateTime.Now,
                HomePhone = "HomePhone" + id,
                InternalCode = "InternalCode" + id,
                IsCriticalPosition = true,
                MaritalStatusId = id,
                MobilePhone = "MobilePhone" + id,
                ResidenceCity = "ResidenceCity" + id,
                RetirementDate = DateTime.Now,
                Surname = "Surname" + id
            };
        }

        public static Rate CreateRate(int id = 0)
        {
            return new Rate
            {
                Id = id,
                Name = "Name" + id,
                Price = 1,
                EmployeePercentage = 1,
                SplitFare = true,

                Client = CreateClient(id),
                Activity = CreateActivity(id),
                VehicleType = CreateVehicleType(id)
            };
        }

        public static RateView CreateRateView(int id = 0)
        {
            return new RateView
            {
                Id = id,
                Name = "Name" + id,
                ActivityName = "Name" + id,
                ClientName = "Name" + id,
                Price = 1,
                EmployeePercentage = 1,
                SplitFare = true,
                VehicleTypeName = "Name" + id
            };
        }

        public static RateCreateEditView CreateRateCreateEditView(int id = 0)
        {
            return new RateCreateEditView
            {
                Id = id,
                Name = "Name" + id,
                ActivityId = id,
                ClientId = id,
                Price = 1,
                EmployeePercentage = 1,
                SplitFare = true,
                VehicleTypeId = id
            };
        }

        #endregion Operation

        #region Tests

        public static TestModel CreateTestModel(int id = 0)
        {
            return new TestModel
            {
                Title = "Title" + id
            };
        }

        #endregion Tests
    }
}
