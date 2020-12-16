using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Reflection;
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


            AircraftCarrier = new Boat(0, 0, 5, BoardSize);
            Battleship = new Boat(0, 0, 4, BoardSize);
            Submarine = new Boat(0, 0, 3, BoardSize);
            Cruiser = new Boat(0, 0, 3, BoardSize);
            Destroyer = new Boat(0, 0, 2, BoardSize);

            Boats.AddRange(new IBoat[] { Destroyer, Cruiser, Submarine, Battleship, AircraftCarrier });
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
        public bool ValidBoatPlacement => CheckValidBoatPlacement();
        public int BoardSize { get; } = 9;

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

        public void UpdateValidBoatPlacement()
        {
            OnPropertyChanged(nameof(ValidBoatPlacement));
        }

        /// <summary>
        /// Switches selected boat to the next in the list, is circular
        /// </summary>
        private void SwitchSelectedBoat()
        {
            SelectedBoat = Boats[(Boats.IndexOf(SelectedBoat) + 1) % Boats.Count];
            SetSelectedBoatEnabled();
            //OnPropertyChanged(nameof(ValidBoatPlacement));
        }
        private void SetSelectedBoatEnabled()
        {
            if(Boats.Find(x => x.IsSelected) != null) { Boats.Find(x => x.IsSelected).IsSelected = false; }
            Boats.Find(x => x == SelectedBoat).IsSelected = true;
        }
        private bool CheckValidBoatPlacement()
        {
            // using string/serialized coord because List.Contains 
            // peforms refrence comparison not value comparison
            List<String> occupiedSpaces = new List<String>();
            foreach (IBoat boat in Boats)
            {
                foreach (Coordinate coord in boat.CoordinateRange.GetAllCoordinates())
                {
                    if (occupiedSpaces.Contains(coord.ToString()))
                    {
                        return false;
                    }
                    else
                    {
                        occupiedSpaces.Add(coord.ToString());
                    }
                }
            }
            return true;
        }

    }
}
