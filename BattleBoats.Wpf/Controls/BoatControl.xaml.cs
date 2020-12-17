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

namespace BattleBoats.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for BoatControl.xaml
    /// </summary>
    public partial class BoatControl : UserControl
    {
        public double BoatWidth
        {
            get { return (double)GetValue(BoatWidthProperty); }
            set { SetValue(BoatWidthProperty, value); }
        }
        public double BoatHeight
        {
            get { return (double)GetValue(BoatHeightProperty); }
            set { SetValue(BoatHeightProperty, value); }
        }
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }


        public bool ShowBoat
        {
            get { return (bool)GetValue(ShowBoatProperty); }
            set { SetValue(ShowBoatProperty, value); }
        }

        public static readonly DependencyProperty ShowBoatProperty =
            DependencyProperty.Register(nameof(ShowBoat), typeof(bool), typeof(BoatControl), new PropertyMetadata(false));



        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(BoatControl), 
            new PropertyMetadata(false));

        public static readonly DependencyProperty BoatHeightProperty = DependencyProperty.Register(
            nameof(BoatHeight),
            typeof(double),
            typeof(BoatControl),
            new PropertyMetadata(0d));

        public static readonly DependencyProperty BoatWidthProperty = DependencyProperty.Register(
            nameof(BoatWidth),
            typeof(double),
            typeof(BoatControl),
            new PropertyMetadata(0d));

        public BoatControl()
        {
            InitializeComponent();
        }
    }
}
