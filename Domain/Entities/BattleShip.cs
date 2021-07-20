using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum Orientation
    {
        Horizontal, Vertical
    }
    public class BattleShip
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
