using AppLogistics.Data.Core;
using AppLogistics.Objects;
using AppLogistics.Tests;
using System;
using Xunit;

namespace AppLogistics.Validators.Tests
{
    public class NoveltyValidatorTests : IDisposable
    {
        private NoveltyValidator validator;
        private TestingContext context;
        private Novelty novelty;

        public NoveltyValidatorTests()
        {
            context = new TestingContext();
            validator = new NoveltyValidator(new UnitOfWork(new TestingContext(context)));

            context.Set<Novelty>().Add(novelty = ObjectsFactory.CreateNovelty());
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            validator.Dispose();
        }

        #region CanCreate(NoveltyView view)

        [Fact]
        public void CanCreate_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanCreate(ObjectsFactory.CreateNoveltyView(1)));
        }

        [Fact]
        public void CanCreate_ValidNovelty()
        {
            Assert.True(validator.CanCreate(ObjectsFactory.CreateNoveltyView(1)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion

        #region CanEdit(NoveltyView view)

        [Fact]
        public void CanEdit_InvalidState_ReturnsFalse()
        {
            validator.ModelState.AddModelError("Test", "Test");

            Assert.False(validator.CanEdit(ObjectsFactory.CreateNoveltyView(novelty.Id)));
        }

        [Fact]
        public void CanEdit_ValidNovelty()
        {
            Assert.True(validator.CanEdit(ObjectsFactory.CreateNoveltyView(novelty.Id)));
            Assert.Empty(validator.ModelState);
            Assert.Empty(validator.Alerts);
        }

        #endregion
    }
}
