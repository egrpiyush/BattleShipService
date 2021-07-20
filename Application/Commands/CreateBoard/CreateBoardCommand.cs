using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.CreateBoard
{
    public class CreateBoardCommand : IRequest<Unit>
    {
        public class Handler : IRequestHandler<CreateBoardCommand, Unit>
        {
            private readonly IBoard _board;
            public Handler(IBoard board)
            {
                _board = board;
            }
            public async Task<Unit> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}
