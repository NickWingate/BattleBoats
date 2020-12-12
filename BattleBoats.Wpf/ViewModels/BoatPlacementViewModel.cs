using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class BoatPlacementViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand MoveBoatCommand { get; }
        // need to define for each boat
        public ICommand RotateBoatCommand { get; set; }

        public BoatPlacementViewModel(INavigator navigator)
        {
            Boat1 = new Boat(5, 6, 3, 9);

            _navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveBoatCommand = new MoveBoatCommand(this, nameof(Boat1));
            RotateBoatCommand = new RelayCommand(Boat1.Rotate);
        }

        public Boat Boat1 { get; set; }
    }
}
