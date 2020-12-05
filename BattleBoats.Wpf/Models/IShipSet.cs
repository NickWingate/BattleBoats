using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public interface IShipSet
    {
        public string Name { get; set; }
        public bool Sunk { get; set; }
        public Coordinate Location { get; set; }
        public int Length { get; set; }
        public int Health { get; set; }
    }
}
