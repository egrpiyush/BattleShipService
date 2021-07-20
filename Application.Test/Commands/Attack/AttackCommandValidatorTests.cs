using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands.Attack;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Test.Commands.Attack
{
    public class AttackCommandValidatorTests
    {
        private static readonly Fixture Fixture = new Fixture();
        private readonly AttackCommandValidator _rulesValidator;
        public AttackCommandValidatorTests()
        {
            _rulesValidator = new AttackCommandValidator();
        }

        [Fact]
        public void WhenXCoordinatesForStartingLocationIsInvalid_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = -1, Y = 0 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.Location.X);
        }

        [Fact]
        public void WhenXCoordinatesForStartingLocationIs10_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 10, Y = 0 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.Location.X);
        }

        [Fact]
        public void WhenYCoordinatesForStartingLocationIsInvalid_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = -1 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.Location.Y);
        }

        [Fact]
        public void WhenYCoordinatesForStartingLocationIs10_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AttackCommand>()
                .With(p => p.Location, new Domain.Entities.Location { X = 0, Y = 10 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.Location.Y);
        }
    }
}
