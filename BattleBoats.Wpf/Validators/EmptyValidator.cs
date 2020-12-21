using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Validators
{
    public class EmptyValidator<T> : IValidator<T>
    {
        public bool Validate(T t)
        {
            return true;
        }

        public bool Validate(T t, T extra)
        {
            return true;
        }
    }
}
