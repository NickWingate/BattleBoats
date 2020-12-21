using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class CoordinateRange
    {
        public CoordinateRange(Coordinate startCoord, Coordinate endCoord)
        {
            StartCoord = startCoord;
            EndCoord = endCoord;
            
        }

        public Coordinate StartCoord { get; set; }
        public Coordinate EndCoord { get; set; }

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
