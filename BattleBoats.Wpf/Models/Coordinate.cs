using BattleBoats.Wpf.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Coordinate : ObservableObject
    {
        private readonly int _length;
        private readonly IValidator<int> _validator;

        private int _xCoord;
        public int XCoord
        {
            get { return _xCoord; }
            set 
            {
                if (_validator.Validate(value, _length))
                {
                    _xCoord = value;
                    OnPropertyChanged(nameof(XCoord));
                }
            }
        }

        private int _yCoord;
        public int YCoord
        {
            get { return _yCoord; }
            set 
            {
                if (_validator.Validate(value, _length))
                {
                    _yCoord = value;
                    OnPropertyChanged(nameof(YCoord));
                }
            }
        }



        public Coordinate(int xCoord, int yCoord, IValidator<int> validator, int length)
        {
            _length = length;
            _validator = validator;
            XCoord = xCoord;
            YCoord = yCoord;
        }
        public Coordinate(int xCoord, int yCoord)
        {
            _validator = new EmptyValidator<int>();
            XCoord = xCoord;
            YCoord = yCoord;
        }
        public override string ToString()
        {
            return $"{XCoord}, {YCoord}";
        }
    }
}
