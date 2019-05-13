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

        #endregion

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

        #endregion

        #region Tests

        public static TestModel CreateTestModel(int id = 0)
        {
            return new TestModel
            {
                Title = "Title" + id
            };
        }

        #endregion
    }
}
