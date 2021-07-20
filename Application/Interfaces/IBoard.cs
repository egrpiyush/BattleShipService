using System;
using System.Collections.Generic;
using System.Text;
using Application.Commands.SharedModels;

namespace Application.Interfaces
{
    public interface IBoard
    {
        List<BattleShipModel> BattleShips { get; set; }
    }
}
