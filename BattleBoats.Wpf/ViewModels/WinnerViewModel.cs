using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class WinnerViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get;}
        public Player Winner { get; }

        public WinnerViewModel(INavigator navigator, Player winner)
        {
            _navigator = navigator;
            Winner = winner;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }
    }
}
