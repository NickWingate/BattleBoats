using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Boat : ObservableObject, IBoat
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
            _maxGridDimention = maxGridDimention;
            _rotated = false;
            Length = length;
            Health = length;
            IsSelected = false;
            Column = column;
            Row = row;
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set 
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
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
                    UpdateCoords();
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
                if (!(value + RowSpan > _maxGridDimention) && value >= 0)
                {
                    _row = value;
                    OnPropertyChanged(nameof(Row));
                    UpdateCoords();
                }
            }
        }

        public Coordinate StartCoord => new Coordinate(Row, Column);
        public Coordinate EndCoord => new Coordinate((Row + RowSpan) - 1, (Column + ColumnSpan) - 1);
        public CoordinateRange CoordinateRange => new CoordinateRange(StartCoord, EndCoord);

        public int RowSpan => _rotated ? 1 : Length;
        public int ColumnSpan => _rotated ? Length : 1;

        public int Length { get; set; }
        public int Health { get; set; }

        public void UpdateCoords()
        {
            OnPropertyChanged(nameof(StartCoord));
            OnPropertyChanged(nameof(EndCoord));
            OnPropertyChanged(nameof(CoordinateRange));
        }
        public void Rotate()
        {
            _rotated = !_rotated;
            OnPropertyChanged(nameof(RowSpan));
            OnPropertyChanged(nameof(ColumnSpan));
            UpdateCoords();
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
