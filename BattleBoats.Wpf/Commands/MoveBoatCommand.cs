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
    }
    public class MoveBoatCommand : ICommand 
    {
        public event EventHandler CanExecuteChanged;
        private Boat _boat;
        private BoatPlacementViewModel _shipPlacementViewModel;

        public MoveBoatCommand(BoatPlacementViewModel shipPlacementViewModel, string boatName)
        {
            _shipPlacementViewModel = shipPlacementViewModel;
            // Get boat of the string that was placed in
            _boat = (Boat)typeof(BoatPlacementViewModel).GetProperty(boatName).GetValue(shipPlacementViewModel);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
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
                    default:
                        break;
                }
            }
        }
    }
}
