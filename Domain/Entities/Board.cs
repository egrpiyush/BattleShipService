using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Board
    {
        public List<BattleShip> BattleShips { get; set; } = new List<BattleShip>();
    }
}
