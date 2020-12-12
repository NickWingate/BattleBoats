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
        /// <param name="column">Column where boat is located (0 based)</param>
        /// <param name="row">Row where boat is located (0 based)</param>
        /// <param name="length">Length of boat (1 based)</param>
        /// <param name="maxGridDimention">Maximum x by x square grid(x is 1 based)</param>
        public Boat(int column, int row, int length, int maxGridDimention)
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
                // If moving the boat will still be in the grid
                if (!(value + ColumnSpan > _maxGridDimention) && value >= 0)
                {
                    _column = value;
                    OnPropertyChanged(nameof(Column));
                }   
            }
        }

        private int _row;
        public int Row
        {
            get { return _row; }
            set 
            {
                // If moving the boat will still be in the grid
                if (!(value + RowSpan  > _maxGridDimention) && value >= 0)
                {
                    _row = value;
                    OnPropertyChanged(nameof(Row));
                }
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
            
            // Stop boat from rotating off board
            if (RowSpan + Row > _maxGridDimention)
            {
                // -1 as row is zero based, but length is 1 based
                Row -= Length - 1;
            }
            else if (ColumnSpan + Column > _maxGridDimention)
            {
                // -1 as column is zero based, but length is 1 based
                Column -= Length - 1;
            }
        }
    }
}
