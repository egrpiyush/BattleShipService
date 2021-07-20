using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands.AddBattleShip;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Test.Commands.AddBattleShip
{
    public class AddBattleShipCommandValidatorTests
    {
        private static readonly Fixture Fixture = new Fixture();
        private readonly AddBattleShipCommandValidator _rulesValidator;
        public AddBattleShipCommandValidatorTests()
        {
            _rulesValidator = new AddBattleShipCommandValidator();
        }

        [Fact]
        public void WhenXCoordinatesForStartingLocationIsInvalid_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = -1, Y = 0 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.StartingLocation.X);
        }

        [Fact]
        public void WhenXCoordinatesForStartingLocationIs10_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 10, Y = 0 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.StartingLocation.X);
        }

        [Fact]
        public void WhenYCoordinatesForStartingLocationIsInvalid_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = -1 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.StartingLocation.Y);
        }

        [Fact]
        public void WhenYCoordinatesForStartingLocationIs10_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.StartingLocation, new Domain.Entities.Location { X = 0, Y = 10 })
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.StartingLocation.Y);
        }

        [Fact]
        public void WhenLengthIs0_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Length, 0)
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.Length);
        }

        [Fact]
        public void WhenLengthIs11_ShouldHaveValidationError()
        {
            //Arrange
            var request = Fixture.Build<AddBattleShipCommand>()
                .With(p => p.Length, 0)
                .Create();
            //Act
            var result = _rulesValidator.TestValidate(request);
            //Assert
            result.ShouldHaveValidationErrorFor(p => p.Length);
        }
    }
}
