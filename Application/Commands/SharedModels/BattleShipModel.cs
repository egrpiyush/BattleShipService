using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Commands.SharedModels
{
    public class BattleShipModel
    {
        public Orientation Orientation { get; set; }
        public Location StartingLocation { get; set; }
        public int Length { get; set; }
        public List<Location> Locations { get; private set; } = new List<Location>();
        public int Hits { get; set; }
        public bool IsSunk
        {
            get
            {
                return Hits >= Length;
            }
        }
    }
}
