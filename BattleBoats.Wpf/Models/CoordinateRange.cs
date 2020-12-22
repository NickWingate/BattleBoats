using BattleBoats.Wpf.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class CoordinateRange : ObservableObject
    {
        private readonly IValidator<Coordinate> _validator;


        public CoordinateRange(Coordinate startCoord, Coordinate endCoord, IValidator<Coordinate> validator)
        {
            StartCoord = startCoord;
            EndCoord = endCoord;
            _validator = validator;
        }

        private Coordinate _startCoord;
        public Coordinate StartCoord 
        {
            get { return _startCoord; }
            set
            {
                if(_validator.Validate(StartCoord, new object[] { }))
                _startCoord = value;
                OnPropertyChanged(nameof(StartCoord));
            } 
        }

        private Coordinate _endCoord;

        public Coordinate EndCoord 
        {
            get { return _endCoord; }
            set
            {
                _endCoord = value;
                OnPropertyChanged(nameof(EndCoord));
            } 
        }

        /// <summary>
        /// Get all the coordinates that the boat occupies
        /// </summary>
        /// <returns></returns>
        public List<Coordinate> GetAllCoordinates()
        {
            List<Coordinate> coords = new List<Coordinate>();
            int width = Math.Abs(EndCoord.XCoord - StartCoord.XCoord) + 1;
            int height = Math.Abs(EndCoord.YCoord - StartCoord.YCoord) + 1;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    coords.Add(new Coordinate(StartCoord.XCoord + i, StartCoord.YCoord + j));
                }
            }
            return coords;
        }

    }
}
