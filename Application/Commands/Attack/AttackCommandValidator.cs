using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Commands.Attack
{
    public class AttackCommandValidator : AbstractValidator<AttackCommand>
    {
        readonly int boardLowerBound = 0;
        readonly int boardUpperBound = 9;
        public AttackCommandValidator()
        {
            RuleFor(p => p.Location)
                .NotNull()
                .WithMessage("Location is required.");

            RuleFor(p => p.Location.X)
                .GreaterThanOrEqualTo(boardLowerBound)
                .WithMessage($"Minimum value should be {boardLowerBound}.");

            RuleFor(p => p.Location.X)
                .LessThanOrEqualTo(boardUpperBound)
                .WithMessage($"Maximum value should be {boardUpperBound}.");

            RuleFor(p => p.Location.Y)
                .GreaterThanOrEqualTo(boardLowerBound)
                .WithMessage($"Minimum value should be {boardLowerBound}.");

            RuleFor(p => p.Location.Y)
                .LessThanOrEqualTo(boardUpperBound)
                .WithMessage($"Maximum value should be {boardUpperBound}.");
        }
    }
}
