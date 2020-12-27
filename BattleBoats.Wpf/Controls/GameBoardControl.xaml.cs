using BattleBoats.Wpf.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BattleBoats.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoardControl : Grid, INotifyPropertyChanged
    {
        public GameBoardControl()
        {
            InitializeComponent();
            CreateBoard();
            Target = new Target(0, 0, BoardDimention) { ShowItem = false };
        }

        public ObservableCollection<IGameItem> HitMarkers
        {
            get { return (ObservableCollection<IGameItem>)GetValue(HitMarkersProperty); }
            set { SetValue(HitMarkersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HitMarkers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HitMarkersProperty =
            DependencyProperty.Register(nameof(HitMarkers), typeof(ObservableCollection<IGameItem>), typeof(GameBoardControl), 
                new PropertyMetadata(null, (dependencyObject, e) => ((GameBoardControl)dependencyObject).OnPropertyChanged(nameof(HitMarkers))));


        public int BoardDimention
        {
            get { return (int)GetValue(BoardDimentionProperty); }
            set { SetValue(BoardDimentionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoardDimention.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoardDimentionProperty =
            DependencyProperty.Register(nameof(BoardDimention), typeof(int), typeof(GameBoardControl), 
                new PropertyMetadata(9, OnBoardDimentionPropertyChanged));



        public List<IBoat> Boats
        {
            get { return (List<IBoat>)GetValue(BoatsProperty); }
            set { SetValue(BoatsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Boats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoatsProperty =
            DependencyProperty.Register(nameof(Boats), typeof(List<IBoat>), typeof(GameBoardControl),
                new PropertyMetadata(new List<IBoat> { new Boat(0, 0, 0, 0) { ShowItem = false } }, OnBoatsPropertyChanged));

        public IGameItem Target
        {
            get { return (IGameItem)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(nameof(Target), typeof(IGameItem), typeof(GameBoardControl), new PropertyMetadata(null));

        private static void OnBoatsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GameBoardControl gameBoardControl = dependencyObject as GameBoardControl;
            gameBoardControl.OnPropertyChanged(nameof(Boats));
            gameBoardControl.CreateBoats();
        }
        private static void OnBoardDimentionPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GameBoardControl gameBoardControl = dependencyObject as GameBoardControl;
            gameBoardControl.OnPropertyChanged(nameof(BoardDimention));
            gameBoardControl.CreateBoard();
        }

        private void CreateBoard()
        {
            //BoardGrid = new Grid();
            for (int i = 0; i < BoardDimention; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                BoardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
            for (int i = 0; i < BoardDimention; i++)
            {
                for (int j = 0; j < BoardDimention; j++)
                {
                    // Add square every other tile
                    if ((i + j) % 2 == 0)
                    {
                        Rectangle rectangle = new Rectangle
                        {
                            Fill = new SolidColorBrush(Colors.Silver),
                            Width = 50,
                            Height = 50,
                            RadiusX = 10,
                            RadiusY = 10,
                        };

                        Grid.SetRow(rectangle, i);
                        Grid.SetColumn(rectangle, j);

                        BoardGrid.Children.Add(rectangle);
                    }
                }
            }
            //MainGrid.Children.Add(BoardGrid);
        }

        private void CreateBoats()
        {
            foreach (IBoat boat in Boats)
            {
                BoatControl boatControl = new BoatControl();

                Binding height = new Binding(nameof(BoatControl.BoatHeightProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("RowSpan")
                };

                Binding width = new Binding(nameof(BoatControl.BoatWidthProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("ColumnSpan")
                };

                Binding column = new Binding(nameof(ColumnProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("Column")
                };

                Binding row = new Binding(nameof(RowProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("Row")
                };

                Binding columnSpan = new Binding(nameof(ColumnSpanProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("ColumnSpan")
                };

                Binding rowSpan = new Binding(nameof(RowSpanProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("RowSpan")
                };

                Binding isSelected = new Binding(nameof(BoatControl.IsSelectedProperty))
                {
                    Source = boat,
                    Path = new PropertyPath("IsSelected")
                };

                Binding isEnabled = new Binding(nameof(BoatControl.ShowBoat))
                {
                    Source = boat,
                    Path = new PropertyPath("ShowItem")
                };

                boatControl.SetBinding(BoatControl.BoatHeightProperty, height);
                boatControl.SetBinding(BoatControl.BoatWidthProperty, width);
                boatControl.SetBinding(ColumnProperty, column);
                boatControl.SetBinding(RowProperty, row);
                boatControl.SetBinding(ColumnSpanProperty, columnSpan);
                boatControl.SetBinding(RowSpanProperty, rowSpan);
                boatControl.SetBinding(BoatControl.IsSelectedProperty, isSelected);
                boatControl.SetBinding(BoatControl.ShowBoatProperty, isEnabled);

                /// <summary>
                /// sets the first boat ontop of the board
                /// e.g. Boats[1] is the 2nd boat and there are 5 boats: 5 - 2 + 1 = 4
                /// hence the 4th highest of all the boats, "+ 1" is to be ontop of the checkered tiles
                /// </summary>
                Panel.SetZIndex(boatControl, Boats.Count - Boats.IndexOf(boat) + 1);
                BoardGrid.Children.Add(boatControl);
            }
        }

        private void DrawHitMarkers()
        {
            if (HitMarkers == null)
            {
                return;
            }
            foreach (IGameItem hitMarker in HitMarkers)
            {
                Ellipse marker = new Ellipse()
                {
                    Width = 20,
                    Height = 20,
                    Fill = ((HitMarker)hitMarker).TileState == TileState.Hit ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.White),
                    Visibility = hitMarker.ShowItem ? Visibility.Visible : Visibility.Hidden
                };
                //temp
                Panel.SetZIndex(marker, 100);
                Grid.SetRow(marker, hitMarker.Row);
                Grid.SetColumn(marker, hitMarker.Column);
                BoardGrid.Children.Add(marker);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == HitMarkersProperty.Name)
            {
                if (HitMarkers != null && HitMarkers is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= Collection_CollectionChanged;
                    collection.CollectionChanged += Collection_CollectionChanged;
                }

            }
        }
        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DrawHitMarkers();
        }
    }
}