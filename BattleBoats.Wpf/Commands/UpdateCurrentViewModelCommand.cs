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
                        _navigator.CurrentViewModel = new MenuViewModel(_navigator);
                        break;
                    case ViewType.Rules:
                        _navigator.CurrentViewModel = new RulesViewModel(_navigator);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
