using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.ViewModels
{
    public interface IBoatViewModel
    {

        public IGameItem SelectedItem { get; set; }
        public int BoardDimention { get; }
    }
}
