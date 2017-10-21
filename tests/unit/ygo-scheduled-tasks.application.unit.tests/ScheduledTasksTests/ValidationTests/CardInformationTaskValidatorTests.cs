using System;
using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.ValidationTests
{
    [TestFixture]
    public class CardInformationTaskValidatorTests
    {
        private CardInformationTaskValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardInformationTaskValidator();
        }

        [TestCaseSource(nameof(_invalidCategories))]
        public void Given_An_CardInformationTask_Categories_Validation_Should_Fail(List<string> categories)
        {
            // Arrange
            var inputModel = new CardInformationTask{ Categories = categories };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.Categories, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_BanlistInformationTask_PageSize_Validation_Should_Fail(int pageSize)
        {
            // Arrange
            var inputModel = new CardInformationTask { PageSize = pageSize };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.PageSize, inputModel);

            // Assert
            act.Invoke();
        }

        #region private helpers

        static object[] _invalidCategories =
        {
            new object[] { null },
            new object[] { new List<string>() }
        };

        #endregion
    }
}