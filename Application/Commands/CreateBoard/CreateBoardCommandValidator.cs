using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Commands.CreateBoard
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator()
        {

        }
    }
}
