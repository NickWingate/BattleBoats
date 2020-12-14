using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class BoatPlacementViewModel : BaseViewModel, IBoatViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand MoveBoatCommand { get; set; }
        public ICommand SwitchSelectedBoatCommand { get; set; }

        public BoatPlacementViewModel(INavigator navigator)
        {
            _navigator = navigator;

            AircraftCarrier = new Boat(0, 0, 5, 9);
            Destroyer = new Boat(4, 4, 2, 9);

            Boats.AddRange(new IBoat[] { AircraftCarrier, Destroyer });
            SelectedBoat = Boats[0];
            SetSelectedBoatEnabled();

            SwitchSelectedBoatCommand = new RelayCommand(SwitchSelectedBoat);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveBoatCommand = new MoveBoatCommand(this, nameof(SelectedBoat));
        }

        public List<IBoat> Boats { get; set; } = new List<IBoat>();
        private IBoat _selectedBoat;

        public IBoat SelectedBoat
        {
            get { return _selectedBoat; }
            set 
            {
                _selectedBoat = value;
                OnPropertyChanged(nameof(SelectedBoat));
            }
        }

        // Length 5
        public IBoat AircraftCarrier { get; set; }
        // Length 4
        public IBoat Battleship { get; set; }
        // Length 3
        public IBoat Submarine { get; set; }
        // Length 3
        public IBoat Cruiser { get; set; }
        // Length 2
        public IBoat Destroyer { get; set; }

        /// <summary>
        /// Switches selected boat to the next in the list, is circular
        /// </summary>
        private void SwitchSelectedBoat()
        {
            SelectedBoat = Boats[(Boats.IndexOf(SelectedBoat) + 1) % Boats.Count];
            SetSelectedBoatEnabled();
        }
        private void SetSelectedBoatEnabled()
        {
            if(Boats.Find(x => x.IsSelected) != null) { Boats.Find(x => x.IsSelected).IsSelected = false; }
            Boats.Find(x => x == SelectedBoat).IsSelected = true;
        }
    }
}
