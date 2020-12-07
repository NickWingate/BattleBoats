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
            NewGameCommand = new RelayCommand(NewGame);
            LoadGameCommand = new RelayCommand(LoadGame);
            ReadRulesCommand = new RelayCommand(ReadRules);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }
        public ICommand NewGameCommand { get; set; }
        public ICommand LoadGameCommand { get; set; }
        public ICommand ReadRulesCommand { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        private void NewGame()
        {
            Debug.WriteLine("New Game Button Clicked");
        }
        private void LoadGame()
        {
            Debug.WriteLine("Load Game Button Clicked");
        }
        private void ReadRules()
        {
            Debug.WriteLine("Read Rules Button Clicked");
        }
    }
}
