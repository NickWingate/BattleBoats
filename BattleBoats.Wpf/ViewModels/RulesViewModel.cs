using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class RulesViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public RulesViewModel(INavigator navigator)
        {
            Navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }
    }
}
