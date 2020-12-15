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
    public class BoatPlacementViewModelTests
    {
        [TestMethod()]
        public void ValidBoatPlacementTest()
        {
            BoatPlacementViewModel boatPlacementViewModel= new BoatPlacementViewModel(new Navigator());
            boatPlacementViewModel.Boats = new List<IBoat>
            {
                new Boat(0, 0, 3, 9),
                new Boat(3, 3, 2, 9),
                //new Boat(0, 0, 3, 9),
            };

            bool expected = true;
            bool acctual = boatPlacementViewModel.ValidBoatPlacement;
            
            Assert.AreEqual(expected, acctual);
        }
    }
}