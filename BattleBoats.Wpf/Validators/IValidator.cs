using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Validators
{
    public interface IValidator<T>
    {
        public bool Validate(T t);
        public bool Validate(T t, object[] args);
    }
}
