using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.ComputerAlgorithm
{
    public interface IComputerAlgorithmService
    {
        public Coordinate NextShot(TileState[,] gameBoard);
    }
}
