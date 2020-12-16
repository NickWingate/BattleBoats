using BattleBoats.Wpf.Services.Navigation;
using BattleBoats.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.Menu:
                        _navigator.Navigate(new MenuViewModel(_navigator));
                        break;
                    case ViewType.BoatPlacement:
                        _navigator.Navigate(new BoatPlacementViewModel(_navigator));
                        break;
                    case ViewType.Rules:
                        _navigator.Navigate(new RulesViewModel(_navigator));
                        break;
                    case ViewType.Game:
                        _navigator.Navigate(new GameViewModel(_navigator, null));
                        break;
                    case ViewType.Winner:
                        _navigator.Navigate(new WinnerViewModel(_navigator));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
