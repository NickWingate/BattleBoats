using BattleBoats.Wpf.Commands;
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
        public ShipPlacementViewModel(INavigator navigator)
        {
            _navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }
        public ShipPlacementViewModel()
        {
            _navigator = new Navigator();
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator);
        }

        private int _boatColumn;

        public int BoatColumn
        {
            get { return _boatColumn; }
            set { _boatColumn = value; }
        }

    }
}
