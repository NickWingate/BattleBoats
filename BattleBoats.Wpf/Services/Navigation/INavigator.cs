using BattleBoats.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleBoats.Wpf.Services.Navigation
{
    public enum ViewType
    {
        Menu,
        Rules,
        ShipPlacement,
        Game,
        Winner
    }
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
    }
}
