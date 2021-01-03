using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.BoatApearanceManager
{
    public class BoatApearanceManager : IBoatApearanceManager
    {
        public BoatApearanceManager()
        {
            _boatsVisible = false;
        }
        public void DeselectBoats(IEnumerable<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.IsSelected = false;
            }
        }

        public void HideBoats(IEnumerable<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = false;
            }
        }

        private bool _boatsVisible;
        public void ToggleBoatView(IEnumerable<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = _boatsVisible;
            }
            _boatsVisible = !_boatsVisible;
        }

        public void CheckForSunkBoats(IEnumerable<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                if (boat.Health == 0)
                {
                    boat.ShowItem = true;
                }
            }
        }
    }
}
