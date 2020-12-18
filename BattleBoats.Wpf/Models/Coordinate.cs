using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Coordinate : ObservableObject
    {
        private int _xCoord;
        public int XCoord
        {
            get { return _xCoord; }
            set 
            { 
                _xCoord = value;
                OnPropertyChanged(nameof(XCoord));
            }
        }

        private int _yCoord;
        public int YCoord
        {
            get { return _yCoord; }
            set 
            {
                _yCoord = value;
                OnPropertyChanged(nameof(YCoord));
            }
        }



        public Coordinate(int xCoord, int yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
        }
        public override string ToString()
        {
            return $"{XCoord}, {YCoord}";
        }
    }
}
