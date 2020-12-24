using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleBoats.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using BattleBoats.Wpf.Services.Navigation;
using BattleBoats.Wpf.Models;

namespace BattleBoats.Wpf.ViewModels.Tests
{
    [TestClass()]
    public class GameViewModelTests
    {
        [TestMethod()]
        public void TransformLocationToGridTest()
        {
            GameViewModel gameViewModel = new GameViewModel(new Navigator(), new List<IBoat>()
            {
                new Boat(0, 0, 2, 4),
                new Boat(1, 0, 3, 4),
            });

            var actual = gameViewModel.TransformLocationToGrid(gameViewModel.UserBoats, 4);
            // 2d array is [y,x] because [row, column], x is column(i.e. horizontal location), y is row(vertical location)
            var expected = new TileState[,]
            {
                { TileState.Boat, TileState.Boat, TileState.Empty, TileState.Empty },
                { TileState.Boat, TileState.Boat, TileState.Boat, TileState.Empty },
                { TileState.Empty, TileState.Empty, TileState.Empty, TileState.Empty },
                { TileState.Empty, TileState.Empty, TileState.Empty, TileState.Empty },
            };
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}