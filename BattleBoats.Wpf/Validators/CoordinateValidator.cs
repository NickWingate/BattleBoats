using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Validators
{
    public class CoordinateValidator : IValidator<int>
    {
        private int _maxValue;

        public CoordinateValidator(int maxValue)
        {
            _maxValue = maxValue;
        }

        public bool Validate(int t)
        {
            return (t <= _maxValue && t >= 0);
        }

        /// <param name="length"> e.g. column span</param>
        public bool Validate(int t, int length)
        {
            return (t + length <= _maxValue && t >= 0);
        }
    }
}
