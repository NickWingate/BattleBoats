using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Target : ObservableObject, IGameItem
    {
        private int _maxGridDimention = 9;

        private int _row;

        public int Row
        {
            get { return _row; }
            set 
            {
                // + 1 is for the length of the target
                if (!(value + 1 > _maxGridDimention) && value >= 0)
                {
                    _row = value;
                    OnPropertyChanged(nameof(Row));
                }
            }
        }


        private int _column;

        public int Column
        {
            get { return _column; }
            set 
            {
                // + 1 is for the length of the target
                if (!(value + 1 > _maxGridDimention) && value >= 0)
                {
                    _column = value;
                    OnPropertyChanged(nameof(Column));
                }
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

        public Coordinate Location { get; }
    }
}
