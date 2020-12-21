using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using BattleBoats.Wpf.Validators;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class BoatPlacementViewModel : BaseViewModel, IBoatViewModel
    {
        private readonly IValidator<int> _validator;
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand MoveBoatCommand { get; set; }
        public ICommand SwitchSelectedBoatCommand { get; set; }
        public ICommand PlayGameCommand { get; set; }

        // Temporary / for testing commands:
        public ICommand SetValidBoatPlacementCommand { get; set; }

        public BoatPlacementViewModel(INavigator navigator, IValidator<int> validator)
        {
            _validator = validator;
            _navigator = navigator;
            Boats = new List<IGameItem>();

            AircraftCarrier = new Boat(0, 0, 5, BoardSize, validator);
            Battleship = new Boat(0, 0, 4, BoardSize, validator);
            Submarine = new Boat(0, 0, 3, BoardSize, validator);
            Cruiser = new Boat(0, 0, 3, BoardSize, validator);
            Destroyer = new Boat(0, 0, 2, BoardSize, validator);

            Boats.AddRange(new IGameItem[] { Destroyer, Cruiser, Submarine, Battleship, AircraftCarrier });
            SelectedItem = Boats[0];
            SetSelectedBoatEnabled();

            SwitchSelectedBoatCommand = new RelayCommand(SwitchSelectedBoat);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveBoatCommand = new MoveBoatCommand(this);
            PlayGameCommand = new RelayCommand(PlayGame);
            SetValidBoatPlacementCommand = new RelayCommand(SetValidBoatPlacement);
        }

        public bool ValidBoatPlacement => CheckValidBoatPlacement();

        public int BoardSize { get; } = 9;

        private List<IGameItem> _boats;
        public List<IGameItem> Boats
        {
            get { return _boats; }
            set 
            {
                _boats = value;
                OnPropertyChanged(nameof(Boats));
            }
        }

        private IGameItem _selectedBoat;
        public IGameItem SelectedItem
        {
            get { return _selectedBoat; }
            set 
            {
                _selectedBoat = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        // Length 5
        public IGameItem AircraftCarrier { get; set; }
        // Length 4
        public IGameItem Battleship { get; set; }
        // Length 3
        public IGameItem Submarine { get; set; }
        // Length 3
        public IGameItem Cruiser { get; set; }
        // Length 2
        public IGameItem Destroyer { get; set; }

        public void UpdateValidBoatPlacement()
        {
            OnPropertyChanged(nameof(ValidBoatPlacement));
        }
        public void PlayGame()
        {
            _navigator.Navigate(new GameViewModel(_navigator, Boats, _validator));
        }

        /// <summary>
        /// Switches selected boat to the next in the list, is circular
        /// </summary>
        private void SwitchSelectedBoat()
        {
            SelectedItem = Boats[(Boats.IndexOf(SelectedItem) + 1) % Boats.Count];
            SetSelectedBoatEnabled();
            //OnPropertyChanged(nameof(ValidBoatPlacement));
        }
        private void SetSelectedBoatEnabled()
        {
            if(Boats.Find(x => x.IsSelected) != null) { Boats.Find(x => x.IsSelected).IsSelected = false; }
            Boats.Find(x => x == SelectedItem).IsSelected = true;
        }
        private bool CheckValidBoatPlacement()
        {
            // using string/serialized coord because List.Contains 
            // peforms refrence comparison not value comparison
            List<String> occupiedSpaces = new List<String>();
            foreach (IGameItem boat in Boats)
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
        private void SetValidBoatPlacement()
        {
            for (int i = 0; i < Boats.Count; i++)
            {
                if (Boats[i].Rotated)
                {
                    Boats[i].Rotate();
                }
                Boats[i].Location.XCoord = i;
            }
            UpdateValidBoatPlacement();
        }
    }
}
