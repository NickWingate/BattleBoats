using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Target : ObservableObject, IGameItem
    {

        public Target(int column, int row, int maxGridDimention)
        {
            MaxGridDimention = maxGridDimention;
            Column = column;
            Row = row;
            ShowItem = true;
        }

        public int MaxGridDimention { get; set; }

        private int _row;

        public int Row
        {
            get { return _row; }
            set 
            {
                // + 1 is for the length of the target
                if (!(value + 1 > MaxGridDimention) && value >= 0)
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
                if (!(value + 1 > MaxGridDimention) && value >= 0)
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
