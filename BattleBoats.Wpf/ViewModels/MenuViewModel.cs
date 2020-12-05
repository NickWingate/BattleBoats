using BattleBoats.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel()
        {
            NewGameCommand = new RelayCommand(NewGame);
            LoadGameCommand = new RelayCommand(LoadGame);
            ReadRulesCommand = new RelayCommand(ReadRules);
        }
        public ICommand NewGameCommand { get; set; }
        public ICommand LoadGameCommand { get; set; }
        public ICommand ReadRulesCommand { get; set; }
        
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
