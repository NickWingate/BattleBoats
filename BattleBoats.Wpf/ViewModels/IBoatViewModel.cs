using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.ViewModels
{
    public interface IBoatViewModel
    {
        public IBoat AircraftCarrier { get; set; }
        public IBoat Battleship { get; set; }
        public IBoat Submarine { get; set; }
        public IBoat Cruiser { get; set; }
        public IBoat Destroyer { get; set; }
        public IBoat SelectedBoat { get; set; }
        public void UpdateValidBoatPlacement();

    }
}
