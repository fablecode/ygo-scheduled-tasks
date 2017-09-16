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

        [Test]
        public void Given_A_CardInformationTask_Categories_Should_Not_Be_Null()
        {
            // Arrange
            var inputModel = new CardInformationTask();

            // Act
            Action act  = () =>  _sut.ShouldHaveValidationErrorFor(ci => ci.Categories, inputModel);

            // Assert
            act.Invoke();
        }

        [Test]
        public void Given_A_CardInformationTask_Categories_Should_Not_Be_Empty()
        {
            // Arrange
            var inputModel = new CardInformationTask{ Categories = new List<string>()};

            // Act
            Action act = () => _sut.ShouldHaveValidationErrorFor(ci => ci.Categories, inputModel);

            // Assert
            act.Invoke();
        }

    }
}