using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class ShipPlacementViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand MoveShipCommand { get; }

        public ShipPlacementViewModel(INavigator navigator)
        {
            Boat1 = new Boat(5, 6);


            _navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveShipCommand = new MoveShipCommand(this, nameof(Boat1));
        }

        public Boat Boat1 { get; set; }
    }
}
