using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AppLogistics.Services.Tests
{
    public class EmployeeServiceTests : IDisposable
    {
        private EmployeeService service;
        private TestingContext context;
        private Employee employee;

        public EmployeeServiceTests()
        {
            context = new TestingContext();
            service = new EmployeeService(new UnitOfWork(new TestingContext(context)));

            context.Set<Employee>().Add(employee = ObjectsFactory.CreateEmployee());
            context.SaveChanges();
        }

        public void Dispose()
        {
            service.Dispose();
            context.Dispose();
        }

        #region Get<TView>(String id)

        [Fact]
        public void Get_ReturnsViewById()
        {
            EmployeeView actual = service.Get<EmployeeView>(employee.Id);
            EmployeeView expected = Mapper.Map<EmployeeView>(employee);

            Assert.Equal(expected.HasDisciplinaryBackground, actual.HasDisciplinaryBackground);
            Assert.Equal(expected.HasInternalRegulations, actual.HasInternalRegulations);
            Assert.Equal(expected.EmergencyContactPhone, actual.EmergencyContactPhone);
            Assert.Equal(expected.HasLaborCertification, actual.HasLaborCertification);
            Assert.Equal(expected.HasPersonalReference, actual.HasPersonalReference);
            Assert.Equal(expected.HasEndownmentLetter, actual.HasEndownmentLetter);
            Assert.Equal(expected.HasCurriculumVitae, actual.HasCurriculumVitae);
            Assert.Equal(expected.IsCriticalPosition, actual.IsCriticalPosition);
            Assert.Equal(expected.HasMilitaryIdCopy, actual.HasMilitaryIdCopy);
            Assert.Equal(expected.EmergencyContact, actual.EmergencyContact);
            Assert.Equal(expected.HasAdmissionTest, actual.HasAdmissionTest);
            Assert.Equal(expected.HasKnowledgeTest, actual.HasKnowledgeTest);
            Assert.Equal(expected.HasDocumentCopy, actual.HasDocumentCopy);
            Assert.Equal(expected.MaritalStatusName, actual.MaritalStatusName);
            Assert.Equal(expected.BranchOfficeName, actual.BranchOfficeName);
            Assert.Equal(expected.DocumentNumber, actual.DocumentNumber);
            Assert.Equal(expected.DocumentTypeName, actual.DocumentTypeName);
            Assert.Equal(expected.RetirementDate, actual.RetirementDate);
            Assert.Equal(expected.ResidenceCity, actual.ResidenceCity);
            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.InternalCode, actual.InternalCode);
            Assert.Equal(expected.HasContract, actual.HasContract);
            Assert.Equal(expected.MobilePhone, actual.MobilePhone);
            Assert.Equal(expected.HasPhotos, actual.HasPhotos);
            Assert.Equal(expected.HomePhone, actual.HomePhone);
            Assert.Equal(expected.BornDate, actual.BornDate);
            Assert.Equal(expected.Comments, actual.Comments);
            Assert.Equal(expected.HireDate, actual.HireDate);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Surname, actual.Surname);
            Assert.Equal(expected.AfpName, actual.AfpName);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.EpsName, actual.EpsName);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region GetViews()

        [Fact]
        public void GetViews_ReturnsEmployeeViews()
        {
            EmployeeView[] actual = service.GetViews().ToArray();
            EmployeeView[] expected = context
                .Set<Employee>()
                .ProjectTo<EmployeeView>()
                .OrderByDescending(view => view.CreationDate)
                .ToArray();

            for (int i = 0; i < expected.Length || i < actual.Length; i++)
            {
                Assert.Equal(expected[i].HasDisciplinaryBackground, actual[i].HasDisciplinaryBackground);
                Assert.Equal(expected[i].HasInternalRegulations, actual[i].HasInternalRegulations);
                Assert.Equal(expected[i].EmergencyContactPhone, actual[i].EmergencyContactPhone);
                Assert.Equal(expected[i].HasLaborCertification, actual[i].HasLaborCertification);
                Assert.Equal(expected[i].HasPersonalReference, actual[i].HasPersonalReference);
                Assert.Equal(expected[i].HasEndownmentLetter, actual[i].HasEndownmentLetter);
                Assert.Equal(expected[i].HasCurriculumVitae, actual[i].HasCurriculumVitae);
                Assert.Equal(expected[i].IsCriticalPosition, actual[i].IsCriticalPosition);
                Assert.Equal(expected[i].HasMilitaryIdCopy, actual[i].HasMilitaryIdCopy);
                Assert.Equal(expected[i].EmergencyContact, actual[i].EmergencyContact);
                Assert.Equal(expected[i].HasAdmissionTest, actual[i].HasAdmissionTest);
                Assert.Equal(expected[i].HasKnowledgeTest, actual[i].HasKnowledgeTest);
                Assert.Equal(expected[i].HasDocumentCopy, actual[i].HasDocumentCopy);
                Assert.Equal(expected[i].MaritalStatusName, actual[i].MaritalStatusName);
                Assert.Equal(expected[i].BranchOfficeName, actual[i].BranchOfficeName);
                Assert.Equal(expected[i].DocumentNumber, actual[i].DocumentNumber);
                Assert.Equal(expected[i].DocumentTypeName, actual[i].DocumentTypeName);
                Assert.Equal(expected[i].RetirementDate, actual[i].RetirementDate);
                Assert.Equal(expected[i].ResidenceCity, actual[i].ResidenceCity);
                Assert.Equal(expected[i].CreationDate, actual[i].CreationDate);
                Assert.Equal(expected[i].InternalCode, actual[i].InternalCode);
                Assert.Equal(expected[i].HasContract, actual[i].HasContract);
                Assert.Equal(expected[i].MobilePhone, actual[i].MobilePhone);
                Assert.Equal(expected[i].HasPhotos, actual[i].HasPhotos);
                Assert.Equal(expected[i].HomePhone, actual[i].HomePhone);
                Assert.Equal(expected[i].BornDate, actual[i].BornDate);
                Assert.Equal(expected[i].Comments, actual[i].Comments);
                Assert.Equal(expected[i].HireDate, actual[i].HireDate);
                Assert.Equal(expected[i].Address, actual[i].Address);
                Assert.Equal(expected[i].Surname, actual[i].Surname);
                Assert.Equal(expected[i].AfpName, actual[i].AfpName);
                Assert.Equal(expected[i].Email, actual[i].Email);
                Assert.Equal(expected[i].EpsName, actual[i].EpsName);
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        #endregion

        #region Create(EmployeeView view)

        [Fact]
        public void Create_Employee()
        {
            EmployeeCreateEditView view = ObjectsFactory.CreateEmployeeCreateEditView(1);
            view.Id = 0;

            service.Create(view);

            Employee actual = context.Set<Employee>().AsNoTracking().Single(model => model.Id != employee.Id);
            EmployeeCreateEditView expected = view;

            Assert.Equal(expected.HasDisciplinaryBackground, actual.HasDisciplinaryBackground);
            Assert.Equal(expected.HasInternalRegulations, actual.HasInternalRegulations);
            Assert.Equal(expected.EmergencyContactPhone, actual.EmergencyContactPhone);
            Assert.Equal(expected.HasLaborCertification, actual.HasLaborCertification);
            Assert.Equal(expected.HasPersonalReference, actual.HasPersonalReference);
            Assert.Equal(expected.HasEndownmentLetter, actual.HasEndownmentLetter);
            Assert.Equal(expected.HasCurriculumVitae, actual.HasCurriculumVitae);
            Assert.Equal(expected.IsCriticalPosition, actual.IsCriticalPosition);
            Assert.Equal(expected.HasMilitaryIdCopy, actual.HasMilitaryIdCopy);
            Assert.Equal(expected.EmergencyContact, actual.EmergencyContact);
            Assert.Equal(expected.HasAdmissionTest, actual.HasAdmissionTest);
            Assert.Equal(expected.HasKnowledgeTest, actual.HasKnowledgeTest);
            Assert.Equal(expected.HasDocumentCopy, actual.HasDocumentCopy);
            Assert.Equal(expected.MaritalStatusId, actual.MaritalStatusId);
            Assert.Equal(expected.BranchOfficeId, actual.BranchOfficeId);
            Assert.Equal(expected.DocumentNumber, actual.DocumentNumber);
            Assert.Equal(expected.DocumentTypeId, actual.DocumentTypeId);
            Assert.Equal(expected.RetirementDate, actual.RetirementDate);
            Assert.Equal(expected.ResidenceCity, actual.ResidenceCity);
            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.InternalCode, actual.InternalCode);
            Assert.Equal(expected.HasContract, actual.HasContract);
            Assert.Equal(expected.MobilePhone, actual.MobilePhone);
            Assert.Equal(expected.HasPhotos, actual.HasPhotos);
            Assert.Equal(expected.HomePhone, actual.HomePhone);
            Assert.Equal(expected.BornDate, actual.BornDate);
            Assert.Equal(expected.Comments, actual.Comments);
            Assert.Equal(expected.HireDate, actual.HireDate);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Surname, actual.Surname);
            Assert.Equal(expected.AfpId, actual.AfpId);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.EpsId, actual.EpsId);
            Assert.Equal(expected.Name, actual.Name);
        }

        #endregion

        #region Edit(EmployeeView view)

        [Fact(Skip = "Need to check execution order?")]
        public void Edit_Employee()
        {
            EmployeeCreateEditView view = ObjectsFactory.CreateEmployeeCreateEditView(employee.Id);
            view.Address = "Address0";
            view.AfpId = 1;
            view.BornDate = DateTime.Today;
            view.BranchOfficeId = 1;
            view.Comments = "Comments0";
            view.DocumentNumber = "DocumentNumber0";
            view.DocumentTypeId = 1;
            view.Email = "Email0";
            view.EmergencyContact = "EmergencyContact0";
            view.EmergencyContactPhone = "EmergencyContactPhone0";
            view.EpsId = 1;
            view.HasAdmissionTest = true;
            view.HasContract = true;
            view.HasCurriculumVitae = true;
            view.HasDisciplinaryBackground = true;
            view.HasDocumentCopy = true;
            view.HasEndownmentLetter = true;
            view.HasInternalRegulations = true;
            view.HasInternalRegulations = true;
            view.HasKnowledgeTest = true;
            view.HasLaborCertification = true;
            view.HasMilitaryIdCopy = true;
            view.HasPersonalReference = true;
            view.HasPhotos = true;
            view.HireDate = DateTime.Today;
            view.HomePhone = "HomePhone0";
            view.InternalCode = "InternalCode0";
            view.IsCriticalPosition = true;
            view.MaritalStatusId = 1;
            view.Name = "Name0";
            view.MobilePhone = "MobilePhone0";
            view.ResidenceCity = "ResidenceCity0";
            view.RetirementDate = DateTime.Today;
            view.Surname = "Surname0";

            service.Edit(view);

            Employee actual = context.Set<Employee>().AsNoTracking().Single();
            Employee expected = employee;

            Assert.Equal(expected.HasDisciplinaryBackground, actual.HasDisciplinaryBackground);
            Assert.Equal(expected.HasInternalRegulations, actual.HasInternalRegulations);
            Assert.Equal(expected.EmergencyContactPhone, actual.EmergencyContactPhone);
            Assert.Equal(expected.HasLaborCertification, actual.HasLaborCertification);
            Assert.Equal(expected.HasPersonalReference, actual.HasPersonalReference);
            Assert.Equal(expected.HasEndownmentLetter, actual.HasEndownmentLetter);
            Assert.Equal(expected.HasCurriculumVitae, actual.HasCurriculumVitae);
            Assert.Equal(expected.IsCriticalPosition, actual.IsCriticalPosition);
            Assert.Equal(expected.HasMilitaryIdCopy, actual.HasMilitaryIdCopy);
            Assert.Equal(expected.EmergencyContact, actual.EmergencyContact);
            Assert.Equal(expected.HasAdmissionTest, actual.HasAdmissionTest);
            Assert.Equal(expected.HasKnowledgeTest, actual.HasKnowledgeTest);
            Assert.Equal(expected.HasDocumentCopy, actual.HasDocumentCopy);
            Assert.Equal(expected.MaritalStatusId, actual.MaritalStatusId);
            Assert.Equal(expected.BranchOfficeId, actual.BranchOfficeId);
            Assert.Equal(expected.DocumentNumber, actual.DocumentNumber);
            Assert.Equal(expected.DocumentTypeId, actual.DocumentTypeId);
            Assert.Equal(expected.RetirementDate, actual.RetirementDate);
            Assert.Equal(expected.ResidenceCity, actual.ResidenceCity);
            Assert.Equal(expected.CreationDate, actual.CreationDate);
            Assert.Equal(expected.InternalCode, actual.InternalCode);
            Assert.Equal(expected.HasContract, actual.HasContract);
            Assert.Equal(expected.MobilePhone, actual.MobilePhone);
            Assert.Equal(expected.HasPhotos, actual.HasPhotos);
            Assert.Equal(expected.HomePhone, actual.HomePhone);
            Assert.Equal(expected.BornDate, actual.BornDate);
            Assert.Equal(expected.Comments, actual.Comments);
            Assert.Equal(expected.HireDate, actual.HireDate);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.Surname, actual.Surname);
            Assert.Equal(expected.AfpId, actual.AfpId);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.EpsId, actual.EpsId);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Id, actual.Id);
        }

        #endregion

        #region Delete(String id)

        [Fact]
        public void Delete_Employee()
        {
            service.Delete(employee.Id);

            Assert.Empty(context.Set<Employee>());
        }

        #endregion
    }
}
