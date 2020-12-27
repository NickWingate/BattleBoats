﻿using BattleBoats.Wpf.Commands;
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
        public ICommand MoveGameItemCommand { get; set; }
        public ICommand SwitchSelectedBoatCommand { get; set; }
        public ICommand PlayGameCommand { get; set; }

        // Temporary / for testing commands:
        public ICommand SetValidBoatPlacementCommand { get; set; }

        public BoatPlacementViewModel(INavigator navigator)
        {
            _navigator = navigator;
            Boats = new List<IBoat>();

            AircraftCarrier = new Boat(0, 0, 5, BoardDimention);
            Battleship = new Boat(0, 0, 4, BoardDimention);
            Submarine = new Boat(0, 0, 3, BoardDimention);
            Cruiser = new Boat(0, 0, 3, BoardDimention);
            Destroyer = new Boat(0, 0, 2, BoardDimention);

            Boats.AddRange(new IBoat[] { Destroyer, Cruiser, Submarine, Battleship, AircraftCarrier });
            SelectedItem = Boats[0];
            SetSelectedBoatEnabled();

            SwitchSelectedBoatCommand = new RelayCommand(SwitchSelectedBoat);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveGameItemCommand = new MoveGameItemCommand(this);
            PlayGameCommand = new RelayCommand(PlayGame);
            SetValidBoatPlacementCommand = new RelayCommand(SetValidBoatPlacement);
        }

        public bool ValidBoatPlacement => CheckValidBoatPlacement();

        public int BoardDimention => Settings.BoardDimention;

        private List<IBoat> _boats;
        public List<IBoat> Boats
        {
            get { return _boats; }
            set 
            {
                _boats = value;
                OnPropertyChanged(nameof(Boats));
            }
        }

        private IGameItem _selectedItem;
        public IGameItem SelectedItem
        {
            get { return _selectedItem; }
            set 
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
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

        public void UpdateValidPlacement()
        {
            OnPropertyChanged(nameof(ValidBoatPlacement));
        }
        public void PlayGame()
        {
            _navigator.Navigate(new GameViewModel(_navigator, Boats));
        }

        /// <summary>
        /// Switches selected boat to the next in the list, is circular
        /// </summary>
        private void SwitchSelectedBoat()
        {
            SelectedItem = Boats[(Boats.IndexOf((IBoat)SelectedItem) + 1) % Boats.Count];
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
        private void SetValidBoatPlacement()
        {
            for (int i = 0; i < Boats.Count; i++)
            {
                if (Boats[i].Rotated)
                {
                    Boats[i].Rotate();
                }
                Boats[i].Column = i;
            }
            UpdateValidPlacement();
        }
    }
}
