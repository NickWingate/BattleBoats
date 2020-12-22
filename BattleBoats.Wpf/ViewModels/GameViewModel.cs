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
        public ICommand ToggleCPUBoatViewCommand { get; set; }
        public ICommand UserShootCommand { get; set; }

        public GameViewModel(INavigator navigator, List<IBoat> boats)
        {
            _navigator = navigator;
            UserBoats = boats;
            Target = new Target { Column = 0, Row = 0, ShowItem = true };
            SelectedItem = Target;

            ComputerBoats = GenerateComputerBoats();
            DeselectBoats(UserBoats);
            HideBoats(ComputerBoats);
            // transform list of locations to 2d array/map of boats

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveBoatCommand = new MoveBoatCommand(this);
            ToggleCPUBoatViewCommand = new RelayCommand(() => ToggleCPUBoatView(ComputerBoats));
            //UserShootCommand = 
        }

        public IGameItem Target { get; set; }
        public IGameItem SelectedItem { get; set; }
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
                boat.ShowItem = false;
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
        private void ToggleCPUBoatView(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = !boat.ShowItem;
            }
        }
        private void UserShoot()
        {
            // get target location
            Coordinate targetLocation = Target.Location;
            // x and o can be IGameItems?
            // if ship: display red 'x' on grid
            // if empty: display green 'o' on grid
        }
        //private bool CheckForBoat(List<IGameItem> boats, Coordinate location)
        //{
        //    
        //}
    }
}
