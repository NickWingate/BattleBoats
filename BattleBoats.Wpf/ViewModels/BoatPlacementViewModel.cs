﻿using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.BoatPlacement;
using BattleBoats.Wpf.Services.BoatApearanceManager;
using BattleBoats.Wpf.Services.ComputerAlgorithm;
using BattleBoats.Wpf.Services.ListToGridTransformer;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BattleBoats.Wpf.Services.SaveGame;

namespace BattleBoats.Wpf.ViewModels
{
    public class BoatPlacementViewModel : BaseViewModel, IBoatViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; set; }
        public ICommand MoveGameItemCommand { get; set; }
        public ICommand SwitchSelectedBoatCommand { get; set; }
        public ICommand PlayGameCommand { get; set; }

        // Temporary / for testing commands:
        public ICommand SetValidBoatPlacementCommand { get; set; }

        public BoatPlacementViewModel(INavigator navigator)
        {
            _navigator = navigator;
            User = new Player("User");

            User.Boats = new ObservableCollection<IBoat>
            {
                new Boat(0, 0, 2, BoardDimention),
                new Boat(0, 0, 3, BoardDimention),
                new Boat(0, 0, 3, BoardDimention),
                new Boat(0, 0, 4, BoardDimention),
                new Boat(0, 0, 5, BoardDimention)
            };
            SelectedItem = User.Boats[0];
            SetSelectedBoatEnabled();
            AssignCommands(navigator);
        }

        private void AssignCommands(INavigator navigator)
        {
            SwitchSelectedBoatCommand = new RelayCommand(SwitchSelectedBoat);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveGameItemCommand = new MoveGameItemCommand(this);
            PlayGameCommand = new RelayCommand(PlayGame);
            SetValidBoatPlacementCommand = new RelayCommand(SetValidBoatPlacement);
        }

        public bool ValidBoatPlacement => CheckValidBoatPlacement();

        public int BoardDimention => Settings.BoardDimention;

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

        public Player User { get; set; }

        public void UpdateValidPlacement()
        {
            OnPropertyChanged(nameof(ValidBoatPlacement));
        }
        public void PlayGame()
        {
            // TEMPORARY need to choose difficulty of computer algorithm and do proper dependency injection with IoC container
            _navigator.Navigate(
                new GameViewModel(
                    _navigator, 
                    new RandomShootingAlgorithm(BoardDimention),
                    new BoatPlacementGenerator(),
                    new ListToGridTransformer(),
                    new BoatApearanceManager(),
                    new SaveGameService(),
                    User)
                );
        }

        /// <summary>
        /// Switches selected boat to the next in the list, is circular
        /// </summary>
        private void SwitchSelectedBoat()
        {
            SelectedItem = User.Boats[(User.Boats.IndexOf((IBoat)SelectedItem) + 1) % User.Boats.Count];
            SetSelectedBoatEnabled();
        }
        private void SetSelectedBoatEnabled()
        {
            foreach (IBoat boat in User.Boats)
            {
                if (boat == SelectedItem)
                {
                    boat.IsSelected = true;
                }
                else
                {
                    boat.IsSelected = false;
                }
            }
        }
        private bool CheckValidBoatPlacement()
        {
            // using string/serialized coord because List.Contains 
            // peforms refrence comparison not value comparison
            List<String> occupiedSpaces = new List<String>();
            foreach (IBoat boat in User.Boats)
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
            for (int i = 0; i < User.Boats.Count; i++)
            {
                if (User.Boats[i].Rotated)
                {
                    User.Boats[i].Rotate();
                }
                User.Boats[i].Column = i;
            }
            UpdateValidPlacement();
        }
    }
}
