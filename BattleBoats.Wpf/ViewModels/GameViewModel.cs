using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Controls;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public enum TileState
    {
        Boat,
        Empty,
        Hit,
        Miss,
    }
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
            ComputerBoats = GenerateComputerBoats();

            UserGameBoard = TransformLocationToGrid(UserBoats, UserBoats[0].MaxGridDimention);
            ComputerGameBoard = TransformLocationToGrid(ComputerBoats, ComputerBoats[0].MaxGridDimention);

            Target = new Target(0, 0, BoardDimention);
            SelectedItem = Target;



            DeselectBoats(UserBoats);
            HideBoats(ComputerBoats);
            // transform list of locations to 2d array/map of boats

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveBoatCommand = new MoveBoatCommand(this);
            ToggleCPUBoatViewCommand = new RelayCommand(() => ToggleBoatView(ComputerBoats));
            //UserShootCommand = 

        }
        public int BoardDimention => Settings.BoardDimention;
        public IGameItem Target { get; set; }
        public IGameItem SelectedItem { get; set; }
        public List<IBoat> UserBoats { get; set; }
        public List<IBoat> ComputerBoats { get; set; }
        public TileState[,] UserGameBoard { get; set; }
        public TileState[,] ComputerGameBoard { get; set; }

        /// <summary>
        /// Deselects every boat in the list
        /// </summary>
        /// <param name="boats"></param>
        private void DeselectBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.IsSelected = false;
            }
        }

        /// <summary>
        /// Sets all the boats in the list to hidden <br/>
        /// Similar to <see cref="ToggleBoatView(List{IBoat})"/>
        /// </summary>
        /// <param name="boats"></param>
        private void HideBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = false;
            }
        }

        /// <summary>
        /// Create a list of boats and their location for the computer
        /// currently this is constant but will be random in the future
        /// </summary>
        /// <returns> List of IBoats for the computers boats</returns>
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

        /// <summary>
        /// Toggles the visibility of all the boats in the list
        /// </summary>
        /// <param name="boats"> the list of boats to effect </param>
        private void ToggleBoatView(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = !boat.ShowItem;
            }
        }

        /// <summary>
        /// Shoot a missile at targets location
        /// </summary>
        private void UserShoot()
        {
            // get target location
            Coordinate targetLocation = Target.Location;
            // x and o can be IGameItems?
            // if ship: display red 'x' on grid
            // if empty: display green 'o' on grid
        }

        /// <summary>
        /// Transforms list of boats, with their locations into a 2D array of boats, empty, and hit/miss tiles
        /// </summary>
        /// <param name="boats"> The list of boats </param>
        /// <param name="gridSize"> Size of the 2D array (1 based and square)</param>
        /// <returns> 2D array of TileState[,] type </returns>
        private TileState[,] TransformLocationToGrid(List<IBoat> boats, int gridSize)
        {
            TileState[,] gameGrid = new TileState[gridSize, gridSize];
            Populate2DArray(ref gameGrid, TileState.Empty);
            foreach (IBoat boat in boats)
            {
                foreach (Coordinate coordinate in boat.CoordinateRange.GetAllCoordinates())
                {
                    gameGrid[coordinate.XCoord, coordinate.YCoord] = TileState.Boat;
                }
            }
            return gameGrid;
        }

        private bool HitShip(TileState[,] gameBoard, Coordinate coordinate)
        {
            return gameBoard[coordinate.YCoord, coordinate.XCoord] == TileState.Boat;
        }

        /// <summary>
        /// Fills 2D array with one value of type T
        /// </summary>
        /// <typeparam name="T"> Type of array </typeparam>
        /// <param name="array"> 2D array to fill </param>
        /// <param name="value"> Value to fill array as </param>
        /// <returns></returns>
        private void Populate2DArray<T>(ref T[,] array, T value)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = value;
                }
            }
        }
    }
}
