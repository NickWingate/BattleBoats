﻿using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.BoatPlacement;
using BattleBoats.Wpf.Services.ComputerAlgorithm;
using BattleBoats.Wpf.Services.ListToGridTransformer;
using BattleBoats.Wpf.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleBoats.Wpf.ViewModels
{
    public enum Player
    {
        User,
        Computer,
    }
    public class GameViewModel : BaseViewModel, IBoatViewModel
    {
        private readonly INavigator _navigator;
        private readonly IComputerAlgorithmService _computerAlgorithm;
        private readonly IBoatPlacementGenerationService _boatPlacementGenerator;
        private readonly IListToGridTransformer _listToGridTransformer;
        private Player _currentPlayersTurn;
        private CancellationTokenSource _userShootTokenSource;

        public ICommand UpdateCurrentViewModelCommand { get; set; }
        public ICommand MoveGameItemCommand { get; set; }
        public ICommand ToggleCPUBoatViewCommand { get; set; }
        public ICommand UserShootCommand { get; set; }


        public GameViewModel(INavigator navigator, 
                             IComputerAlgorithmService computerAlgorithm, 
                             IBoatPlacementGenerationService boatPlacementGenerator,
                             IListToGridTransformer listToGridTransformer,
                             List<IBoat> boats)
        {
            // Dependency Injection
            _navigator = navigator;
            _computerAlgorithm = computerAlgorithm;
            _boatPlacementGenerator = boatPlacementGenerator;
            _listToGridTransformer = listToGridTransformer;

            // Assign fields and properties
            _currentPlayersTurn = Player.User;
            AssignBoats(boats);
            AssignCommands(navigator);
            AssignTarget();

            // Prep data
            TransformBoatsToBoard();
            PrepBoats();

            // Start Game
            BeginGameplay();
        }


        private ObservableCollection<IGameItem> _computerHitMarkers = new ObservableCollection<IGameItem>();
        public ObservableCollection<IGameItem> ComputerHitMarkers
        {
            get { return _computerHitMarkers; }
            set
            {
                _computerHitMarkers = value;
                OnPropertyChanged(nameof(ComputerHitMarkers));
            }
        }

        private ObservableCollection<IGameItem> _userHitMarkers = new ObservableCollection<IGameItem>();
        public ObservableCollection<IGameItem> UserHitMarkers
        {
            get { return _userHitMarkers; }
            set
            {
                _userHitMarkers = value;
                OnPropertyChanged(nameof(UserHitMarkers));
            }
        }

        private bool _canUserShoot = true;
        public bool CanUserShoot
        {
            get { return _canUserShoot && ValidTargetLocation; }
            set 
            {
                _canUserShoot = value;
                OnPropertyChanged(nameof(CanUserShoot));
            }
        }

        public bool GameCompleted { get; set; } = false;
        public int BoardDimention => Settings.BoardDimention;
        public bool ValidTargetLocation { get; private set; } = true;
        public IGameItem Target { get; set; }
        public IGameItem SelectedItem { get; set; }
        public List<IBoat> UserBoats { get; set; }
        public List<IBoat> ComputerBoats { get; set; }
        public TileState[,] UserGameBoard { get; set; }
        public TileState[,] ComputerGameBoard { get; set; }
        public int UserHealth 
        {
            get
            {
                int total = 0;
                foreach (IBoat boat in UserBoats)
                {
                    total += boat.Health;
                }
                return total;
            }

        }
        public int ComputerHealth
        {
            get
            {
                int total = 0;
                foreach (IBoat boat in ComputerBoats)
                {
                    total += boat.Health;
                }
                return total;
            }

        }


        /// <summary>
        /// Assign User and Computer IBoat lists
        /// </summary>
        /// <param name="userBoats"> User's boat list </param>
        private void AssignBoats(List<IBoat> userBoats)
        {
            UserBoats = userBoats;
            ComputerBoats = _boatPlacementGenerator.GenerateBoats(5, BoardDimention);
        }

        /// <summary>
        /// Transforms List of IBoats to 2D array of TileStates, which functions as a grid/board
        /// </summary>
        private void TransformBoatsToBoard()
        {
            UserGameBoard = _listToGridTransformer.TransformLocationToGrid(UserBoats, BoardDimention);
            ComputerGameBoard = _listToGridTransformer.TransformLocationToGrid(ComputerBoats, BoardDimention);
        }

        /// <summary>
        /// Creates target and sets it as SelectedItem
        /// </summary>
        private void AssignTarget()
        {
            Target = new Target(0, 0, BoardDimention);
            SelectedItem = Target;
        }

        /// <summary>
        /// Deselects user boats and hides CPU boats
        /// </summary>
        private void PrepBoats()
        {
            DeselectBoats(UserBoats);
            HideBoats(ComputerBoats);
        }

        /// <summary>
        /// Assings all <see cref="ICommand"/>s
        /// </summary>
        /// <param name="navigator"> navigator needed for <see cref="UpdateCurrentViewModelCommand"/></param>
        private void AssignCommands(INavigator navigator)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveGameItemCommand = new MoveGameItemCommand(this);
            ToggleCPUBoatViewCommand = new RelayCommand(() => ToggleBoatView(ComputerBoats));
            UserShootCommand = new RelayCommand(() => UserShoot());
        }

        /// <summary>
        /// Begins the game
        /// </summary>
        private async void BeginGameplay()
        {
            // Each loop is one players turn
            while (UserHealth > 0 && ComputerHealth > 0)
            {
                // Computer's turn
                if (_currentPlayersTurn == Player.Computer)
                {
                    Shoot(_computerAlgorithm.NextShot(UserGameBoard), UserHitMarkers);
                    _currentPlayersTurn = Player.User;
                }
                // User's turn
                else
                {
                    // TODO: find alternative to try catch for TaskCanceledException
                    try
                    {
                        await WaitForUserPlay();
                    }
                    catch (TaskCanceledException) { }
                }
                //SaveGameState();
            }
            Player winner = UserHealth == 0 ? Player.Computer : Player.User;
            _navigator.Navigate(new WinnerViewModel(_navigator, winner));
        }

        /// <summary>
        /// Creates a <see cref="CancellationTokenSource"/> to be canceled when user completes their turn
        /// </summary>
        /// <returns></returns>
        private Task WaitForUserPlay()
        {
            _userShootTokenSource = new CancellationTokenSource();
            return Task.Delay(-1, _userShootTokenSource.Token);
        }

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
        /// Checks if the target is in a valid tile i.e. not a tile already shot
        /// </summary>
        public void UpdateValidPlacement()
        {
            TileState targetOverTileState = ComputerGameBoard[Target.Row, Target.Column];
            ValidTargetLocation = (targetOverTileState == TileState.Boat || targetOverTileState == TileState.Empty);
            OnPropertyChanged(nameof(CanUserShoot));
        }

        /// <summary>
        /// Shoot a missile at the user's target location
        /// </summary>
        private void UserShoot()
        {
            if (!CanUserShoot)
            {
                return;
            }
            Shoot(Target.Location, ComputerHitMarkers);
            UpdateValidPlacement();
            _currentPlayersTurn = Player.Computer;
            _userShootTokenSource.Cancel();
        }

        /// <summary>
        /// Shoots a missile at location
        /// </summary>
        /// <param name="location"> location to shoot </param>
        /// <param name="hitMarkers"> Collection of hitmarkers to add shot to</param>
        private void Shoot(Coordinate location, ObservableCollection<IGameItem> hitMarkers)
        {
            // get tile state
            TileState tileState = GetAssociatedGameBoard(hitMarkers)[location.YCoord, location.XCoord];

            // if boat: display red 'x' on grid
            if (tileState == TileState.Boat)
            {
                AddHitMarker(location, hitMarkers, TileState.Hit);

                // Get the all of the boats for the associated player
                IEnumerable<IBoat> boats = GetAssociatedBoatCollection(GetAssociatedGameBoard(hitMarkers));

                // Locates the boat that has a coordinate equal to the coordinate being shot i.e. get the boat being shot
                IBoat boatToHit = (from boat in boats
                                    from coord in boat.CoordinateRange.GetAllCoordinates()
                                    where coord.ToString() == location.ToString()
                                    select boat).ElementAt(0);
                boatToHit.Health--;

                // TODO: better way of doing this
                OnPropertyChanged(nameof(UserHealth));
                OnPropertyChanged(nameof(ComputerHealth));

                CheckForSunkBoats(boats);
            }
            // if empty: display green 'o' on grid
            else if (tileState == TileState.Empty)
            {
                AddHitMarker(location, hitMarkers, TileState.Miss);
            }
        }

        /// <summary>
        /// Checks if boat collection has any sunk boats and makes them visible
        /// </summary>
        /// <param name="boats"> Boat colletion </param>
        private void CheckForSunkBoats(IEnumerable<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                if (boat.Health == 0)
                {
                    boat.ShowItem = true;
                }
            }
        }

        /// <summary>
        /// Adds a hit marker to (visual)board and collection of hitMarkers
        /// </summary>
        /// <param name="location"> location of hit marker </param>
        /// <param name="hitMarkers"> ObservableCollection of the hit markers </param>
        /// <param name="tileState"> If tile is hit or miss </param>
        private void AddHitMarker(Coordinate location, ObservableCollection<IGameItem> hitMarkers, TileState tileState)
        {
            hitMarkers.Add(new HitMarker(location, tileState));
            UpdateGrid(hitMarkers);
        }

        /// <summary>
        /// Adds a hit marker to (visual)board and collection of hitMarkers
        /// </summary>
        /// <param name="hitMarker"> New hitmarker to add to list </param>
        /// <param name="hitMarkers"> ObservableCollection of the hit markers </param>
        private void AddHitMarker(HitMarker hitMarker, ObservableCollection<IGameItem> hitMarkers)
        {
            hitMarkers.Add(hitMarker);
            UpdateGrid(hitMarkers);
        }

        /// <summary>
        /// Returns the GameBoard/2d array of the provided collection of hitmarkers
        /// </summary>
        /// <param name="hitMarkers"> Collection of hitmarkers </param>
        /// <returns></returns>
        private TileState[,] GetAssociatedGameBoard(ObservableCollection<IGameItem> hitMarkers)
        {
            if (Object.ReferenceEquals(hitMarkers, ComputerHitMarkers)) 
            {
                return ComputerGameBoard; 
            }
            else if (Object.ReferenceEquals(hitMarkers, UserHitMarkers)) 
            {
                return UserGameBoard; 
            }
            else 
            {
                throw new ArgumentException($"Unrecognised collection: {hitMarkers}"); 
            }
        }

        /// <summary>
        /// Gets the associated collection of boats associated with the game board
        /// </summary>
        /// <param name="gameBoard"></param>
        /// <returns></returns>
        private IEnumerable<IBoat> GetAssociatedBoatCollection(TileState[,] gameBoard)
        {
            if (Object.ReferenceEquals(gameBoard, ComputerGameBoard))
            {
                return ComputerBoats;
            }
            else if (Object.ReferenceEquals(gameBoard, UserGameBoard))
            {
                return UserBoats;
            }
            else
            {
                throw new ArgumentException($"Unrecognised board: {gameBoard}");
            }
        }

        /// <summary>
        /// Adds all hitmarkers to the visual board
        /// </summary>
        /// <param name="boardHitMarkers"></param>
        private void UpdateGrid(ObservableCollection<IGameItem> boardHitMarkers)
        {
            TileState[,] targetGrid = GetAssociatedGameBoard(boardHitMarkers);
            foreach (IGameItem marker in boardHitMarkers)
            {
                targetGrid[marker.Row, marker.Column] = ((HitMarker)marker).TileState;
            }
        }

    }
}