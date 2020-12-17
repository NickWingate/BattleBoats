using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Target : ObservableObject, IGameItem
    {
        private int _row;

        public int Row
        {
            get { return _row; }
            set 
            { 
                _row = value;
                OnPropertyChanged(nameof(Row));
            }
        }


        private int _column;

        public int Column
        {
            get { return _column; }
            set 
            {
                _column = value;
                OnPropertyChanged(nameof(Column));
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
