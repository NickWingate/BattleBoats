﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Boat : ObservableObject, IBoat
    {
        public int MaxGridDimention { get; set; }
        /// <summary>
        /// Create a new boat object
        /// </summary>
        /// <param name="column">Column where boat is located (0 based)</param>
        /// <param name="row">Row where boat is located (0 based)</param>
        /// <param name="length">Length of boat (1 based)</param>
        /// <param name="maxGridDimention">Maximum x by x square grid(x is 1 based)</param>
        public Boat(int column, int row, int length, int maxGridDimention)
        {
            MaxGridDimention = maxGridDimention;
            Rotated = false;
            Length = length;
            Health = length;
            IsSelected = false;
            Column = column;
            Row = row;
            ShowItem = true;
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
                if (!(value + ColumnSpan > MaxGridDimention) && value >= 0)
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
                if (!(value + RowSpan > MaxGridDimention) && value >= 0)
                {
                    _row = value;
                    OnPropertyChanged(nameof(Row));
                    UpdateCoords();
                }
            }
        }

        private bool _rotated;

        public bool Rotated
        {
            get { return _rotated; }
            private set 
            {
                _rotated = value;
                OnPropertyChanged(nameof(Rotated));
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


        public Coordinate Location => new Coordinate(Column, Row);
        public Coordinate EndCoord => new Coordinate((Column + ColumnSpan) - 1, (Row + RowSpan) - 1);
        public CoordinateRange CoordinateRange => new CoordinateRange(Location, EndCoord);

        public int RowSpan => Rotated ? 1 : Length;
        public int ColumnSpan => Rotated ? Length : 1;

        public int Length { get; set; }
        public int Health { get; set; }

        public void UpdateCoords()
        {
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(EndCoord));
            OnPropertyChanged(nameof(CoordinateRange));
        }
        public void Rotate()
        {
            Rotated = !Rotated;
            OnPropertyChanged(nameof(RowSpan));
            OnPropertyChanged(nameof(ColumnSpan));
            UpdateCoords();
            // Stop boat from rotating off board
            if (RowSpan + Row > MaxGridDimention)
            {
                // -1 as row is zero based, but length is 1 based
                Row -= Length - 1;
            }
            else if (ColumnSpan + Column > MaxGridDimention)
            {
                // -1 as column is zero based, but length is 1 based
                Column -= Length - 1;
            }
        }
    }
}
