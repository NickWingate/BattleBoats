using BattleBoats.Wpf.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Boat : ObservableObject, IGameItem
    {
        private readonly IValidator<int> _validator;
        private int _maxGridDimention;
        /// <summary>
        /// Create a new boat object
        /// </summary>
        /// <param name="column">Column where boat is located (0 based)</param>
        /// <param name="row">Row where boat is located (0 based)</param>
        /// <param name="length">Length of boat (1 based)</param>
        /// <param name="maxGridDimention">Maximum x by x square grid(x is 1 based)</param>
        public Boat(int column, int row, int length, int maxGridDimention, IValidator<int> validator)
        {
            _validator = validator;

            _maxGridDimention = maxGridDimention;
            Rotated = false;
            Length = length;
            Health = length;
            IsSelected = false;
            Location = new Coordinate(column, row, validator, length);
            ShowItem = true;
        }
        public Boat(int column, int row, int length, int maxGridDimention)
        {
            _validator = new EmptyValidator<int>();

            _maxGridDimention = maxGridDimention;
            Rotated = false;
            Length = length;
            Health = length;
            IsSelected = false;
            Location = new Coordinate(column, row);
            ShowItem = true;
        }

        private Coordinate _location;

        public Coordinate Location
        {
            get { return _location; }
            set 
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
                UpdateCoords();
            }
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


        private bool _showBoat;

        public bool ShowItem
        {
            get { return _showBoat; }
            set 
            {
                _showBoat = value;
                OnPropertyChanged(nameof(ShowItem));
            }
        }

        public CoordinateRange CoordinateRange => 
            new CoordinateRange(Location, new Coordinate((Location.XCoord + ColumnSpan) - 1, (Location.YCoord + RowSpan) - 1));

        public int RowSpan => Rotated ? 1 : Length;
        public int ColumnSpan => Rotated ? Length : 1;

        public int Length { get; set; }
        public int Health { get; set; }

        public void UpdateCoords()
        {
            OnPropertyChanged(nameof(CoordinateRange));
        }
        public void Rotate()
        {
            Rotated = !Rotated;
            OnPropertyChanged(nameof(RowSpan));
            OnPropertyChanged(nameof(ColumnSpan));
            UpdateCoords();
            // Stop boat from rotating off board
            if (RowSpan + Location.YCoord > _maxGridDimention)
            {
                // -1 as row is zero based, but length is 1 based
                Location.YCoord -= Length - 1;
            }
            else if (ColumnSpan + Location.XCoord > _maxGridDimention)
            {
                // -1 as column is zero based, but length is 1 based
                Location.XCoord -= Length - 1;
            }
        }
    }
}
