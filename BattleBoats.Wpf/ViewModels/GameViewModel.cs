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
            UserBoats = boats;
            ComputerBoats = GenerateComputerBoats();
            DeselectBoats(UserBoats);
            HideBoats(UserBoats);
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        }

        public List<IBoat> UserBoats { get; set; }
        public List<IBoat> ComputerBoats { get; set; }

        private void DeselectBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.IsSelected = false;
            }
        }
        private void HideBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowBoat = false;
            }
        }
        private List<IBoat> GenerateComputerBoats()
        {
            return new List<IBoat>()
            {
                new Boat(8, 0, 5, 9),
                new Boat(7, 0, 4, 9),
                new Boat(6, 0, 3, 9),
                new Boat(5, 0, 3, 9),
                new Boat(4, 0, 2, 9),
            };
        }
    }
}
