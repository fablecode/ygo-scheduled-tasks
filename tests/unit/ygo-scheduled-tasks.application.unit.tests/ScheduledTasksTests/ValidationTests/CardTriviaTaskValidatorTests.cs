using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using ygo_scheduled_tasks.application.ScheduledTasks.CardTrivia;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.ValidationTests
{
    [TestFixture]
    public class CardTriviaTaskValidatorTests
    {
        private CardTriviaTaskValidator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardTriviaTaskValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_CardTriviaTask_Category_Validation_Should_Fail(string category)
        {
            // Arrange
            var inputModel = new CardTriviaTask { Category = category };

            // Act
            Action act  = () =>  _sut.ShouldHaveValidationErrorFor(ci => ci.Category, inputModel);

            // Assert
            act.Invoke();
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Given_An_Invalid_CardTriviaTask_PageSize_Validation_Should_Fail(int pageSize)
        {
            // Arrange
            var inputModel = new CardTriviaTask { PageSize = pageSize };

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.PageSize, inputModel);

            // Assert
            act.Invoke();
        }

    }
}