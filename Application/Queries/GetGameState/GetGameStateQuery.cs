using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Queries.GetGameState
{
    public class GetGameStateQuery : IRequest<GameState>
    {
    }
}
