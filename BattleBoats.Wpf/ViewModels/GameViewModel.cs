using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; }

        public GameViewModel(INavigator navigator)
        {
            _navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }
    }
}
