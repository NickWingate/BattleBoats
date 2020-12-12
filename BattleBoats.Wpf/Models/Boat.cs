using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Boat : ObservableObject
    {
        private int _maxGridDimention;
        private bool _rotated;
        /// <summary>
        /// Create a new boat object
        /// </summary>
        /// <param name="column">Column where boat is located (starting at 0)</param>
        /// <param name="row">Row where boat is located (starting at 0)</param>
        /// <param name="maxGridDimention">Maximum x by x square grid (x starts at 0)</param>
        public Boat(int column, int row, int maxGridDimention, int length)
        {
            _rotated = false;
            Column = column;
            Row = row;
            _maxGridDimention = maxGridDimention;
            Length = length;
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

        public int RowSpan => _rotated ? 1 : Length;
        public int ColumnSpan => _rotated ? Length : 1;

        public int Length { get; set; }



        public void Rotate()
        {
            _rotated = !_rotated;
            OnPropertyChanged(nameof(RowSpan));
            OnPropertyChanged(nameof(ColumnSpan));
        }
    }
}
