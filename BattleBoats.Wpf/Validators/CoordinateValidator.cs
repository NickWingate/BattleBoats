using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Validators
{
    public class CoordinateValidator : IValidator<Coordinate>
    {
        private int _maxValue;
        public CoordinateValidator(int maxValue)
        {
            _maxValue = maxValue;
        }
        public bool Validate(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public bool Validate(Coordinate t, object[] args)
        {
            int length = (int)args[0];
            Coord coord = (Coord)args[1];
            switch (coord)
            {
                case Coord.XCoord:
                    return (t.XCoord + length <= _maxValue && t.XCoord >= 0);
                case Coord.YCoord:
                    return (t.YCoord + length <= _maxValue && t.YCoord >= 0);
                default:
                    throw new Exception($"coord must be \"XCord\" or \"YCoord\" not {coord}");
            }
        }
    }
}
