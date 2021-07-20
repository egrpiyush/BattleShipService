using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.SharedModels;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.AddBattleShip
{
    public class AddBattleShipCommand : IRequest<Unit>
    {
        public Orientation Orientation { get; set; }
        public Location StartingLocation { get; set; }
        public int Length { get; set; }
        
        public class Handler : IRequestHandler<AddBattleShipCommand, Unit>
        {
            private readonly IBoard _board;
            public Handler(IBoard board)
            {
                _board = board;
            }

            public async Task<Unit> Handle(AddBattleShipCommand request, CancellationToken cancellationToken)
            {
                var newBattleShip = new BattleShipModel();
                newBattleShip.Length = request.Length;
                switch (request.Orientation)
                {
                    case Orientation.Horizontal:
                        for (int i = 0; i < request.Length; i++)
                        {
                            newBattleShip.Locations.Add(new Location { X = request.StartingLocation.X, Y = request.StartingLocation.Y + i });
                        }
                        break;
                    case Orientation.Vertical:
                        for (int i = 0; i < request.Length; i++)
                        {
                            newBattleShip.Locations.Add(new Location { X = request.StartingLocation.X + i, Y = request.StartingLocation.Y });
                        }
                        break;
                }

                if (!IsBattleShipLocationValid(newBattleShip))
                    throw new ValidationException("A battle ship already exists at this location.");

                _board.BattleShips.Add(newBattleShip);
                return Unit.Value;
            }

            /// <summary>
            /// Checks if a new battleship is going to be placed over an existing one.
            /// </summary>
            private bool IsBattleShipLocationValid(BattleShipModel newBattleShip)
            {
                foreach (var battleShip in _board.BattleShips)
                {
                    var result = battleShip.Locations.Select(x => new { x.X, x.Y }).Intersect(newBattleShip.Locations.Select(x => new { x.X, x.Y }));
                    if (result != null && result.Any())
                        return false;
                }
                return true;
            }
        }
    }
}
