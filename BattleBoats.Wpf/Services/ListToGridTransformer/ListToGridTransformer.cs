using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.ListToGridTransformer
{
    public class ListToGridTransformer : IListToGridTransformer
    {
        /// <summary>
        /// Transforms list of boats, with their locations into a 2D array of boats, empty, and hit/miss tiles
        /// </summary>
        /// <param name="boats"> The list of boats </param>
        /// <param name="gridSize"> Size of the 2D array (1 based and square)</param>
        /// <returns> 2D array of TileState[,] type </returns>
        public TileState[,] TransformLocationToGrid(IEnumerable<IBoat> boats, int gridSize)
        {
            TileState[,] gameGrid = new TileState[gridSize, gridSize];
            Populate2DArray(ref gameGrid, TileState.Empty);
            foreach (IBoat boat in boats)
            {
                foreach (Coordinate coordinate in boat.CoordinateRange.GetAllCoordinates())
                {
                    gameGrid[coordinate.YCoord, coordinate.XCoord] = TileState.Boat;
                }
            }
            return gameGrid;
        }

        /// <summary>
        /// Fills 2D array with one value of type T
        /// </summary>
        /// <typeparam name="T"> Type of array </typeparam>
        /// <param name="array"> 2D array to fill </param>
        /// <param name="value"> Value to fill array as </param>
        /// <returns></returns>
        private void Populate2DArray<T>(ref T[,] array, T value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }
    }
}
