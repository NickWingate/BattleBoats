using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.ViewModels;

namespace BattleBoats.Wpf.Commands
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
        Rotate,
    }
    public class MoveBoatCommand : ICommand 
    {
        public event EventHandler CanExecuteChanged;
        private IBoat _boat;
        private IBoatViewModel _boatViewModel;

        public MoveBoatCommand(IBoatViewModel boatViewModel, string boatName)
        {
            // Get boat of the string that was placed in
            //_boat = (Boat)typeof(BoatPlacementViewModel).GetProperty(boatName).GetValue(boatViewModel);
            _boat = boatViewModel.SelectedBoat;
            _boatViewModel = boatViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _boat = _boatViewModel.SelectedBoat;
            if (parameter is Direction direction)
            {
                switch (direction)
                {
                    case Direction.North:
                        _boat.Row--;
                        break;
                    case Direction.East:
                        _boat.Column--;
                        break;
                    case Direction.South:
                        _boat.Row++;
                        break;
                    case Direction.West:
                        _boat.Column++;
                        break;
                    case Direction.Rotate:
                        _boat.Rotate();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
