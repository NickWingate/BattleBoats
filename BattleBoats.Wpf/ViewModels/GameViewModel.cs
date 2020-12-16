using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
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

        public GameViewModel(INavigator navigator, List<IBoat> boats)
        {
            _navigator = navigator;
            Boats = boats;
            DeselectBoats(Boats);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }

        public List<IBoat> Boats { get; set; }

        private void DeselectBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.IsSelected = false;
            }
        }
    }
}
