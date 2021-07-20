using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Commands.AddBattleShip
{
    public class AddBattleShipCommandValidator : AbstractValidator<AddBattleShipCommand>
    {
        readonly int boardLowerBound = 0;
        readonly int boardUpperBound = 9;
        public AddBattleShipCommandValidator()
        {
            RuleFor(p => p.Orientation)
                .IsInEnum()
                .WithMessage("Invalid Orientation.");

            RuleFor(p => p.StartingLocation)
                .NotNull()
                .WithMessage("StartingLocation is required.");

            RuleFor(p => p.StartingLocation.X)
                .GreaterThanOrEqualTo(boardLowerBound)
                .WithMessage($"Minimum value should be {boardLowerBound}.");

            RuleFor(p => p.StartingLocation.X)
                .LessThanOrEqualTo(boardUpperBound)
                .WithMessage($"Maximum value should be {boardUpperBound}.");

            RuleFor(p => p.StartingLocation.Y)
                .GreaterThanOrEqualTo(boardLowerBound)
                .WithMessage($"Minimum value should be {boardLowerBound}.");

            RuleFor(p => p.StartingLocation.Y)
                .LessThanOrEqualTo(boardUpperBound)
                .WithMessage($"Maximum value should be {boardUpperBound}.");

            RuleFor(p => p.Length)
                .GreaterThanOrEqualTo(boardLowerBound + 1)
                .WithMessage($"Minimum value should be {boardLowerBound + 1}.");

            RuleFor(p => p.Length)
                .LessThanOrEqualTo(boardUpperBound + 1)
                .WithMessage($"Maximum value should be {boardUpperBound + 1}.");
        }
    }
}
