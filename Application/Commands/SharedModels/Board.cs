using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;

namespace Application.Commands.SharedModels
{
    public class Board : IBoard
    {
        public List<BattleShipModel> BattleShips { get; set; } = new List<BattleShipModel>();
    }
}
