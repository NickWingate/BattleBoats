using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.ListToGridTransformer
{
    public interface IListToGridTransformer
    {
        TileState[,] TransformLocationToGrid(List<IBoat> boats, int gridSize);
    }
}
