using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Boat : ObservableObject
    {
        public Boat(int column, int row)
        {
            Column = column;
            Row = row;
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

    }
}
