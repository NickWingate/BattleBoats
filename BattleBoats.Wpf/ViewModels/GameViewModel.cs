using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public class GameViewModel : BaseViewModel, IBoatViewModel
    {
        private readonly INavigator _navigator;
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand MoveBoatCommand { get; set; }

        public GameViewModel(INavigator navigator, List<IGameItem> boats)
        {
            _navigator = navigator;
            UserBoats = boats;
            Target = new Target { Column = 0, Row = 0, ShowItem = true };
            SelectedItem = Target;

            ComputerBoats = GenerateComputerBoats();
            DeselectBoats(UserBoats);
            HideBoats(ComputerBoats);

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveBoatCommand = new MoveBoatCommand(this);
        }

        public IGameItem Target { get; set; }
        public IGameItem SelectedItem { get; set; }
        public List<IGameItem> UserBoats { get; set; }
        public List<IGameItem> ComputerBoats { get; set; }
       
        private void DeselectBoats(List<IGameItem> boats)
        {
            foreach (IGameItem boat in boats)
            {
                boat.IsSelected = false;
            }
        }
        private void HideBoats(List<IGameItem> boats)
        {
            foreach (IGameItem boat in boats)
            {
                boat.ShowItem = false;
            }
        }
        private List<IGameItem> GenerateComputerBoats()
        {
            return new List<IGameItem>()
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
