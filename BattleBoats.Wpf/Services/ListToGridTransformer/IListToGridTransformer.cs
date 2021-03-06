﻿using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.ListToGridTransformer
{
    public interface IListToGridTransformer
    {
        TileState[,] TransformLocationToGrid(IEnumerable<IBoat> boats, int gridSize);
        TileState[,] TransformLocationToGrid(IEnumerable<IBoat> boats, IEnumerable<IGameItem> hitMarkers, int gridSize);
    }
}
