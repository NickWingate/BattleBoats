using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
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
    public partial class GameBoardControl : Grid
    {
        public GameBoardControl()
        {
            InitializeComponent();
            CreateBoard(9);
            Target = new Target { ShowItem = false };
        }

        public List<IGameItem> Boats
        {
            get { return (List<IGameItem>)GetValue(BoatsProperty); }
            set { SetValue(BoatsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Boats.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoatsProperty =
            DependencyProperty.Register(nameof(Boats), typeof(List<IGameItem>), typeof(GameBoardControl), new PropertyMetadata(null));


        public IGameItem Target
        {
            get { return (IGameItem)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(nameof(Target), typeof(IGameItem), typeof(GameBoardControl), new PropertyMetadata(null));






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
        //private void CreateBoats()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        BoatControl boatControl = new BoatControl();

        //        Binding height = new Binding(nameof(BoatControl.BoatHeightProperty));
        //        height.Source = Boats[i].RowSpan;

        //        Binding width = new Binding(nameof(BoatControl.BoatWidthProperty));
        //        width.Source = boat.ColumnSpan;

        //        Binding column = new Binding(nameof(ColumnProperty));
        //        column.Source = boat.Column;

        //        Binding row = new Binding(nameof(RowProperty));
        //        row.Source = boat.Row;

        //        Binding columnSpan = new Binding(nameof(ColumnSpanProperty));
        //        columnSpan.Source = boat.ColumnSpan;

        //        Binding rowSpan = new Binding(nameof(RowSpanProperty));
        //        rowSpan.Source = boat.RowSpan;

        //        Binding isSelected = new Binding(nameof(BoatControl.IsSelectedProperty));
        //        isSelected.Source = boat.IsSelected;


        //        boatControl.SetBinding(BoatControl.BoatHeightProperty, height);
        //        boatControl.SetBinding(BoatControl.BoatWidthProperty, width);
        //        boatControl.SetBinding(ColumnProperty, column);
        //        boatControl.SetBinding(RowProperty, row);
        //        boatControl.SetBinding(ColumnSpanProperty, columnSpan);
        //        boatControl.SetBinding(RowSpanProperty, rowSpan);
        //        boatControl.SetBinding(BoatControl.IsSelectedProperty, isSelected);

        //        Panel.SetZIndex(boatControl, 1);
        //        BoardGrid.Children.Add(boatControl);
        //    }
        //}
    }
}
