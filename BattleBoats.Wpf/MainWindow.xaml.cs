﻿using BattleBoats.Wpf.Services.Navigation;
using BattleBoats.Wpf.ViewModels;

namespace BattleBoats.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel(this, new Navigator());
        }
    }
}
