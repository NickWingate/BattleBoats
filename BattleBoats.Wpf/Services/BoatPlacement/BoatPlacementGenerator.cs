using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.BoatPlacement
{
    public class BoatPlacementGenerator : IBoatPlacementGenerationService
    {
        public List<IBoat> GenerateBoats(int quantity, int maxDimention)
        {
            int[] lengths = { 5, 4, 3, 3, 2 };
            List<IBoat> computerBoats = new List<IBoat>();
            List<Coordinate> invalidCoordinates = new List<Coordinate>();
            Random rnd = new Random();
            for (int i = 0; i < quantity; i++)
            {
                IBoat newBoat;
                bool validBoat;
                do
                {
                    validBoat = true;
                    int row = rnd.Next(maxDimention + lengths[i] - 1);
                    int column = rnd.Next(maxDimention);

                    newBoat = new Boat(column, row, lengths[i], maxDimention);
                    if (rnd.NextDouble() >= 0.5) newBoat.Rotate();
                    foreach (Coordinate coordinate in newBoat.CoordinateRange.GetAllCoordinates())
                    {
                        foreach (Coordinate invalidCoordinate in invalidCoordinates)
                        {
                            if (coordinate.Equals(invalidCoordinate))
                            {
                                validBoat = false;
                                break;
                            }
                        }
                    }
                } while (!validBoat);
                computerBoats.Add(newBoat);
                invalidCoordinates.AddRange(newBoat.CoordinateRange.GetAllCoordinates());
            }
            return computerBoats;
        }
    }
}
