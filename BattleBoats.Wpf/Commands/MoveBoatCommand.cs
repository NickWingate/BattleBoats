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
    public class MoveBoatCommand : ObservableObject, ICommand 
    {
        public event EventHandler CanExecuteChanged;
        private IGameItem _item;
        private IBoatViewModel _boatViewModel;

        public MoveBoatCommand(IBoatViewModel boatViewModel)
        {
            _item = boatViewModel.SelectedItem;
            _boatViewModel = boatViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _item = _boatViewModel.SelectedItem;
            if (parameter is Direction direction)
            {
                switch (direction)
                {
                    case Direction.North:
                        _item.Location.YCoord--;
                        break;
                    case Direction.East:
                        _item.Location.XCoord--;
                        break;
                    case Direction.South:
                        _item.Location.YCoord++;
                        break;
                    case Direction.West:
                        _item.Location.XCoord++;
                        break;
                    case Direction.Rotate:
                        _item.Rotate();
                        break;
                    default:
                        break;
                }
            }
            if (_boatViewModel.GetType() == typeof(BoatPlacementViewModel))
            {
                ((BoatPlacementViewModel)_boatViewModel).UpdateValidBoatPlacement();
            }
        }
    }
}
