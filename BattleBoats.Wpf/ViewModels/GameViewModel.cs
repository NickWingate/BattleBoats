using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.Services.BoatPlacement;
using BattleBoats.Wpf.Services.BoatApearanceManager;
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
using BattleBoats.Wpf.Services.SaveGame;

namespace BattleBoats.Wpf.ViewModels
{
    public class GameViewModel : BaseViewModel, IBoatViewModel
    {
        private readonly INavigator _navigator;
        private readonly IComputerAlgorithmService _computerAlgorithm;
        private readonly IBoatPlacementGenerationService _boatPlacementGenerator;
        private readonly IListToGridTransformer _listToGridTransformer;
        private readonly IBoatApearanceManager _boatApearanceManager;
        private readonly ISaveGameService _saveGameService;

        private Player _currentPlayersTurn;
        private Player _winner;
        private CancellationTokenSource _userShootTokenSource;

        public ICommand UpdateCurrentViewModelCommand { get; set; }
        public ICommand MoveGameItemCommand { get; set; }
        public ICommand ToggleCPUBoatViewCommand { get; set; }
        public ICommand UserShootCommand { get; set; }
        public ICommand NavigateToWinningViewCommand { get; set; }

        public GameViewModel(INavigator navigator, 
                             IComputerAlgorithmService computerAlgorithm, 
                             IBoatPlacementGenerationService boatPlacementGenerator,
                             IListToGridTransformer listToGridTransformer,
                             IBoatApearanceManager boatApearanceManager,
                             ISaveGameService saveGameService,
                             Player user)
        {
            // Dependency Injection
            _navigator = navigator;
            _computerAlgorithm = computerAlgorithm;
            _boatPlacementGenerator = boatPlacementGenerator;
            _listToGridTransformer = listToGridTransformer;
            _boatApearanceManager = boatApearanceManager;
            _saveGameService = saveGameService;

            // Assign fields and properties
            User = user;
            Computer = new Player(nameof(Computer)) 
            {
                Boats = new ObservableCollection<IBoat>(_boatPlacementGenerator.GenerateBoats(5, BoardDimention))
            };

            _currentPlayersTurn = User;
            AssignCommands(navigator);
            AssignTarget();

            // Prep data
            TransformBoatsToBoard();
            PrepBoats();

            // Start Game
            BeginGameplay();
        }

        public GameViewModel(INavigator navigator,
                             IComputerAlgorithmService computerAlgorithm,
                             IBoatPlacementGenerationService boatPlacementGenerator,
                             IListToGridTransformer listToGridTransformer,
                             IBoatApearanceManager boatApearanceManager,
                             ISaveGameService saveGameService,
                             GameModel game)
        {
            // Dependency Injection
            _navigator = navigator;
            _computerAlgorithm = computerAlgorithm;
            _boatPlacementGenerator = boatPlacementGenerator;
            _listToGridTransformer = listToGridTransformer;
            _boatApearanceManager = boatApearanceManager;
            _saveGameService = saveGameService;

            // Assign fields and properties
            User = game.User;
            Computer = game.Computer;

            _currentPlayersTurn = User;
            AssignCommands(navigator);
            AssignTarget();

            // Prep data
            TransformBoatsToBoard();

            // Start Game
            BeginGameplay();
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


        public Player User { get; set; }
        public Player Computer { get; set; }
        public int UserHealth => User.Health;
        public int ComputerHealth => Computer.Health;
        public bool GameCompleted { get; set; } = false;
        public int BoardDimention => Settings.BoardDimention;
        public bool ValidTargetLocation { get; private set; } = true;
        public IGameItem Target { get; set; }
        public IGameItem SelectedItem { get; set; }
        public string DestinationFilePath { get; set; }


        /// <summary>
        /// Transforms List of IBoats to 2D array of TileStates, which functions as a grid/board
        /// </summary>
        private void TransformBoatsToBoard()
        {
            User.GameBoard = _listToGridTransformer.TransformLocationToGrid(User.Boats, User.HitMarkers, BoardDimention);
            Computer.GameBoard = _listToGridTransformer.TransformLocationToGrid(Computer.Boats, Computer.HitMarkers, BoardDimention);
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
            _boatApearanceManager.DeselectBoats(User.Boats);
            _boatApearanceManager.HideBoats(Computer.Boats);
        }

        /// <summary>
        /// Assings all <see cref="ICommand"/>s
        /// </summary>
        /// <param name="navigator"> navigator needed for <see cref="UpdateCurrentViewModelCommand"/></param>
        private void AssignCommands(INavigator navigator)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
            MoveGameItemCommand = new MoveGameItemCommand(this);
            ToggleCPUBoatViewCommand = new RelayCommand(() => _boatApearanceManager.ToggleBoatView(Computer.Boats));
            UserShootCommand = new RelayCommand(() => UserShoot());
            NavigateToWinningViewCommand = new RelayCommand(() => _navigator.Navigate(new WinnerViewModel(_navigator, _winner)));

        }

        /// <summary>
        /// Begins the game
        /// </summary>
        private async void BeginGameplay()
        {
            // Each loop is one players turn
            while (User.Health > 0 && Computer.Health > 0)
            {
                // Computer's turn
                if (_currentPlayersTurn == Computer)
                {
                    Shoot(_computerAlgorithm.NextShot(User.GameBoard), User);
                    _currentPlayersTurn = User;
                    OnPropertyChanged(nameof(UserHealth));
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
                    OnPropertyChanged(nameof(ComputerHealth));
                }
                _saveGameService.SaveGame(new GameModel(User, Computer), "BattleBoatsGame.json");
            }
            EndGameplay();
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        private void EndGameplay()
        {
            GameCompleted = true;
            CanUserShoot = false;
            Target.ShowItem = false;
            OnPropertyChanged(nameof(GameCompleted));
            _winner = User.Health == 0 ? Computer : User;
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
        /// Checks if the target is in a valid tile i.e. not a tile already shot
        /// </summary>
        public void UpdateValidPlacement()
        {
            TileState targetOverTileState = Computer.GameBoard[Target.Row, Target.Column];
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
            Shoot(Target.Location, Computer);
            UpdateValidPlacement();
            _currentPlayersTurn = Computer;
            _userShootTokenSource.Cancel();
        }

        /// <summary>
        /// Shoots a missile at location
        /// </summary>
        /// <param name="location"> location to shoot </param>
        /// <param name="hitMarkers"> Collection of hitmarkers to add shot to</param>
        private void Shoot(Coordinate location, Player targetPlayer)
        {
            // get tile state
            TileState tileState = targetPlayer.GameBoard[location.YCoord, location.XCoord];

            // if boat: display red 'x' on grid
            if (tileState == TileState.Boat)
            {
                AddHitMarker(location, targetPlayer, TileState.Hit);

                // Locates the boat that has a coordinate equal to the coordinate being shot i.e. get the boat being shot
                IBoat boatToHit = (from boat in targetPlayer.Boats
                                    from coord in boat.CoordinateRange.GetAllCoordinates()
                                    where coord.ToString() == location.ToString()
                                    select boat).ElementAt(0);
                boatToHit.Health--;

                // TODO: better way of doing this
                OnPropertyChanged(nameof(User.Health));
                OnPropertyChanged(nameof(Computer.Health));

                _boatApearanceManager.CheckForSunkBoats(targetPlayer.Boats);
            }
            // if empty: display green 'o' on grid
            else if (tileState == TileState.Empty)
            {
                AddHitMarker(location, targetPlayer, TileState.Miss);
            }
        }

        /// <summary>
        /// Adds a hit marker to (visual)board and collection of hitMarkers
        /// </summary>
        /// <param name="location"> location of hit marker </param>
        /// <param name="hitMarkers"> ObservableCollection of the hit markers </param>
        /// <param name="tileState"> If tile is hit or miss </param>
        private void AddHitMarker(Coordinate location, Player player, TileState tileState)
        {
            player.HitMarkers.Add(new HitMarker(location, tileState));
            UpdateGrid(player);
        }

        /// <summary>
        /// Adds all hitmarkers to the visual board
        /// </summary>
        /// <param name="boardHitMarkers"></param>
        private void UpdateGrid(Player player)
        {
            foreach (IGameItem marker in player.HitMarkers)
            {
                player.GameBoard[marker.Row, marker.Column] = ((HitMarker)marker).TileState;
            }
        }
    }
}