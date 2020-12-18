using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Target : ObservableObject, IGameItem
    {
        private int _maxGridDimention = 9;

        private Coordinate _location;

        public Coordinate Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        

        private bool _showItem;

        public bool ShowItem
        {
            get { return _showItem; }
            set 
            {
                _showItem = value;
                OnPropertyChanged(nameof(ShowItem));
            }
        }


        public int ColumnSpan => 1;

        public int RowSpan => 1;

        public bool IsSelected { get; set; } = true;

        public bool Rotated => false;

        public Coordinate StartCoord { get; }

        public Coordinate EndCoord => StartCoord;

        public CoordinateRange CoordinateRange { get; }

        public void Rotate()
        {
            
        }
    }
}
