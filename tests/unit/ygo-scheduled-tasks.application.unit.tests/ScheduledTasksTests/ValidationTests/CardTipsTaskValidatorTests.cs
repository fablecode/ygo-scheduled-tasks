using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo_scheduled_tasks.application.ScheduledTasks.CardTips;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.ValidationTests
{
    [TestFixture]
    public class CardTipsTaskValidatorTests
    {
        private CardTipsTaskValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardTipsTaskValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_BanlistInformationTask_Category_Validation_Should_Fail(string category)
        {
            // Arrange
            var inputModel = new CardTipsTask { Category = category };

            // Act
            Action act  = () =>  _sut.ShouldHaveValidationErrorFor(ci => ci.Category, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_BanlistInformationTask_PageSize_Validation_Should_Fail(int pageSize)
        {
            // Arrange
            var inputModel = new CardTipsTask { PageSize = pageSize };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.PageSize, inputModel);

            // Assert
            act.Invoke();
        }

    }
}