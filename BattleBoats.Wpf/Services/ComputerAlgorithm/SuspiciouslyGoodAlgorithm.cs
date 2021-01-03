using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.ComputerAlgorithm
{
    public class SuspiciouslyGoodAlgorithm : IComputerAlgorithmService
    {
        private IEnumerable<IBoat> UserBoats;
        private Queue<Coordinate> TilesToShoot = new Queue<Coordinate>();

        public SuspiciouslyGoodAlgorithm(IEnumerable<IBoat> userBoats)
        {
            UserBoats = userBoats;
            QueueBoats();
        }

        public Coordinate NextShot(TileState[,] gameBoard)
        {
            return TilesToShoot.Dequeue();
        }

        private void QueueBoats()
        {
            foreach (IBoat boat in UserBoats)
            {
                foreach (Coordinate coordinate in boat.CoordinateRange.GetAllCoordinates())
                {
                    TilesToShoot.Enqueue(coordinate);
                }
            }
        }

    }
}
