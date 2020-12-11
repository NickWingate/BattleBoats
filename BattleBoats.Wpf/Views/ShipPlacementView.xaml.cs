using BattleBoats.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleBoats.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ShipPlacementView.xaml
    /// </summary>

    public partial class ShipPlacementView : UserControl
    {
        public ShipPlacementView()
        {
            InitializeComponent();
            CreateBoat(5);
        }
        private void CreateBoat(int tileLength)
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
            Grid.SetRow(boat, 0);
            Grid.SetColumn(boat, 0);
            GameBoard.BoardGrid.Children.Add(boat);
        }
    }
}
