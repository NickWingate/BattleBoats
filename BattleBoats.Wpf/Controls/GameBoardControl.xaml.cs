using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            //CreateShip(5);

            //Children = MainGrid.Children;
        }
        public Rectangle Boat { get; set; }
        //public Grid BoardGrid { get; set; }
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
        private void CreateShip(int tileLength)
        {
            Rectangle boat = new Rectangle
            {
                Width = tileLength * 50 - 10,
                Height = 35,
                Fill = new SolidColorBrush(Colors.White),
                RadiusX = 50,
                RadiusY = 20,
            };
            Panel.SetZIndex(boat, 1);
            Grid.SetColumnSpan(boat, tileLength);
            Grid.SetRow(boat, 1);
            Grid.SetColumn(boat, 1);
            BoardGrid.Children.Add(boat);
        }
    }
}
