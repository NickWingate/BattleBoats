using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Validators
{
    public class IntegerCoordinateValidator : IValidator<int>
    {
        private int _maxValue;

        public IntegerCoordinateValidator(int maxValue)
        {
            _maxValue = maxValue;
        }

        public bool Validate(int t)
        {
            return (t <= _maxValue && t >= 0);
        }

        /// <param name="args"> Arguments for the validation, e.g. length</param>
        public bool Validate(int t, object[] args)
        {
            int length = (int)args[0];
            return (t + length <= _maxValue && t >= 0);
        }
    }
}
