using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.ValidationTests
{
    [TestFixture]
    public class BanlistInformationTaskValidatorTests
    {
        private BanlistInformationTaskValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new BanlistInformationTaskValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_BanlistInformationTask_Category_Validation_Should_Fail(string category)
        {
            // Arrange
            var inputModel = new BanlistInformationTask { Category = category };

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
            var inputModel = new BanlistInformationTask { PageSize = pageSize };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.PageSize, inputModel);

            // Assert
            act.Invoke();
        }

    }
}