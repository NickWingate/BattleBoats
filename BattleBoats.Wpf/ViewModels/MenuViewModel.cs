using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; }
        public MenuViewModel(INavigator navigator)
        {
            Navigator = navigator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            LoadGameCommand = new RelayCommand(() => Debug.WriteLine("Load game command pressed"));
        }
        public ICommand LoadGameCommand { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }
    }
}
