using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleBoats.Wpf.Services.ComputerAlgorithm
{
    public class RandomShootingAlgorithm : IComputerAlgorithmService
    {
        private List<Coordinate> ValidTiles = new List<Coordinate>();
        private readonly int _boardSize;

        public RandomShootingAlgorithm(int boardSize)
        {
            _boardSize = boardSize;
            GenerateValidTiles(boardSize);
        }

        public Coordinate NextShot(TileState[,] gameBoard)
        {
            // board is always square
            if (gameBoard.GetLength(0) != gameBoard.GetLength(1))
            {
                throw new ArgumentOutOfRangeException($"gameBoard must have square dimentions " +
                    $"not {gameBoard.GetLength(0)} by {gameBoard.GetLength(1)}");
            }
            if (gameBoard.GetLength(0) != _boardSize)
            {
                throw new ArgumentException("gameBoard is not the same as the size provided in constructor");
            }

            Random rnd = new Random();
            Coordinate randomCoordinate = ValidTiles[rnd.Next(ValidTiles.Count)];
            ValidTiles.Remove(randomCoordinate);
            return randomCoordinate;
        }

        private void GenerateValidTiles(int size)
        {
            CoordinateRange coordinateRange = new CoordinateRange(new Coordinate(0, 0), new Coordinate(size - 1, size -1));
            ValidTiles = coordinateRange.GetAllCoordinates();
        }
    }
}
