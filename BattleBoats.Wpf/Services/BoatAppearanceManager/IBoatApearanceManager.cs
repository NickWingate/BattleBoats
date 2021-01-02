using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.BoatApearanceManager
{
    public interface IBoatApearanceManager
    {
        /// <summary>
        /// Deselects every boat in the list
        /// </summary>
        /// <param name="boats"></param>
        void DeselectBoats(List<IBoat> boats);

        /// <summary>
        /// Sets all the boats in the list to hidden <br/>
        /// Similar to <see cref="ToggleBoatView(List{IBoat})"/>
        /// </summary>
        /// <param name="boats"></param>
        void HideBoats(List<IBoat> boats);

        /// <summary>
        /// Toggles the visibility of all the boats in the list
        /// </summary>
        /// <param name="boats"> the list of boats to effect </param>
        public void ToggleBoatView(List<IBoat> boats);
    }
}
