using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public interface IBoat : IGameItem
    {
        int ColumnSpan { get; }
        int RowSpan { get; }
        int Length { get; set; }
        int Health { get; set; }

        bool IsSelected { get; set; }
        public bool Rotated { get; }

        
        CoordinateRange CoordinateRange { get; }

        void Rotate();

    }
}
