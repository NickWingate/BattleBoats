using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
            CreateBoard(9);
            Target = new Target { ShowItem = false };
        }

        public List<IBoat> Boats
        {
            get { return (List<IBoat>)GetValue(BoatsProperty); }
            set { SetValue(BoatsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Boats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoatsProperty =
            DependencyProperty.Register(nameof(Boats), typeof(List<IBoat>), typeof(GameBoardControl), 
                new PropertyMetadata(new List<IBoat> { new Boat(0, 0, 0, 0) { ShowItem = false} }, OnBoatsPropertyChanged));


        public IGameItem Target
        {
            get { return (IGameItem)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(nameof(Target), typeof(IGameItem), typeof(GameBoardControl), new PropertyMetadata(null));

        private static void OnBoatsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GameBoardControl gameBoardControl = dependencyObject as GameBoardControl;
            gameBoardControl.OnPropertyChanged(nameof(Boats));
            gameBoardControl.OnBoatsPropertyChanged(e);
        }

        private void OnBoatsPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            CreateBoats();
        }
       

        private void CreateBoard(int boardDimention)
        {
            //BoardGrid = new Grid(); 
            //for (int i = 0; i < boardDimention; i++)
            //{
            //    BoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            //    BoardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //}
            for (int i = 0; i < boardDimention; i++)
            {
                for (int j = 0; j < boardDimention; j++)
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

        // Try to create boats dynamically
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

                Panel.SetZIndex(boatControl, 1);
                BoardGrid.Children.Add(boatControl);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
