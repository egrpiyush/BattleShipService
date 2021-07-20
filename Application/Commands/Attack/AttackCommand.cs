using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Attack
{
    public class AttackCommand : IRequest<AttackResponse>
    {
        public Location Location { get; set; }

        public class Handler : IRequestHandler<AttackCommand, AttackResponse>
        {
            private readonly IBoard _board;
            public Handler(IBoard board)
            {
                _board = board;
            }

            public async Task<AttackResponse> Handle(AttackCommand request, CancellationToken cancellationToken)
            {
                var attackResponse = new AttackResponse();
                if (attackResponse.IsGameOver = IsGameOver() == true)
                    return attackResponse;

                foreach (var battleShip in _board.BattleShips)
                {
                    if (battleShip.Locations.Any(p => p.X == request.Location.X && p.Y == request.Location.Y))
                    {
                        battleShip.Hits++;
                        attackResponse.IsAHit = true;
                        break;
                    }
                }

                attackResponse.IsGameOver = IsGameOver();
                return attackResponse;
            }

            private bool IsGameOver()
            {
                return _board.BattleShips.All(p => p.IsSunk);
            }
        }
    }
}
