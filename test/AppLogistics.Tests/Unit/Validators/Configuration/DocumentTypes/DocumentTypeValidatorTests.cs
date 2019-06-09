using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class DocumentTypeValidatorTests : IDisposable
    {
        private DocumentTypeValidator validator;
        private TestingContext context;
        private DocumentType documentType;

        public DocumentTypeValidatorTests()
        {
            context = new TestingContext();
            validator = new DocumentTypeValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<DocumentType>().Add(documentType = ObjectsFactory.CreateDocumentType());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(DocumentTypeView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateDocumentTypeView(1)));
        }

        [Fact]
        public void CanCreate_ValidDocumentType()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateDocumentTypeView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanCreate(DocumentTypeView view)

        #region CanEdit(DocumentTypeView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateDocumentTypeView(documentType.Id)));
        }

        [Fact]
        public void CanEdit_ValidDocumentType()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateDocumentTypeView(documentType.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion CanEdit(DocumentTypeView view)
    }
}
