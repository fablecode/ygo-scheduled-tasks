using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo_scheduled_tasks.application.ScheduledTasks.CardRulings;
using ygo_scheduled_tasks.application.ScheduledTasks.CardTips;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.ValidationTests
{
    [TestFixture]
    public class CardRulingsTaskValidatorTests
    {
        private CardRulingsTaskValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardRulingsTaskValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_CardRulingTask_Category_Validation_Should_Fail(string category)
        {
            // Arrange
            var inputModel = new CardRulingsTask { Category = category };

            // Act
            Action act  = () =>  _sut.ShouldHaveValidationErrorFor(ci => ci.Category, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_CardRulingsTask_PageSize_Validation_Should_Fail(int pageSize)
        {
            // Arrange
            var inputModel = new CardRulingsTask { PageSize = pageSize };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.PageSize, inputModel);

            // Assert
            act.Invoke();
        }

    }
}