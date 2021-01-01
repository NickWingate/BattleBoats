using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public Coordinate(int xCoord, int yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
        }
        public Coordinate()
        {
            XCoord = 0;
            YCoord = 0;
        }
        public override string ToString()
        {
            return $"{XCoord}, {YCoord}";
        }

        public bool Equals([AllowNull] Coordinate other)
        {
            return this.ToString() == other.ToString();
        }
    }
}
