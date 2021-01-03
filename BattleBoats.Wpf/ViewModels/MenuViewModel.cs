using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.BoatApearanceManager;
using BattleBoats.Wpf.Services.BoatPlacement;
using BattleBoats.Wpf.Services.ComputerAlgorithm;
using BattleBoats.Wpf.Services.ListToGridTransformer;
using BattleBoats.Wpf.Services.Navigation;
using BattleBoats.Wpf.Services.SaveGame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        public void LoadGame(string filePath)
        {
            GameModel game;
            string jsonString = File.ReadAllText(filePath);
            game = JsonConvert.DeserializeObject<GameModel>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            Navigator.Navigate(
                new GameViewModel(
                    Navigator,
                    new RandomShootingAlgorithm(Settings.BoardDimention),
                    new BoatPlacementGenerator(),
                    new ListToGridTransformer(),
                    new BoatApearanceManager(),
                    new SaveGameService(),
                    game));
        }
    }
}
