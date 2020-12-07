using BattleBoats.Wpf.Commands;
using BattleBoats.Wpf.Models;
using BattleBoats.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.Services.Navigation
{
    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set 
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
    }
}
