using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.BoatApearanceManager
{
    public class BoatApearanceManager : IBoatApearanceManager
    {
        public void DeselectBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.IsSelected = false;
            }
        }

        public void HideBoats(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = false;
            }
        }

        public void ToggleBoatView(List<IBoat> boats)
        {
            foreach (IBoat boat in boats)
            {
                boat.ShowItem = !boat.ShowItem;
            }
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
