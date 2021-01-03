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
        void DeselectBoats(IEnumerable<IBoat> boats);

        /// <summary>
        /// Sets all the boats in the list to hidden <br/>
        /// Similar to <see cref="ToggleBoatView(IEnumerable{IBoat})"/>
        /// </summary>
        /// <param name="boats"></param>
        void HideBoats(IEnumerable<IBoat> boats);

        /// <summary>
        /// Toggles the visibility of all the boats in the list
        /// </summary>
        /// <param name="boats"> the list of boats to effect </param>
        public void ToggleBoatView(IEnumerable<IBoat> boats);

        /// <summary>
        /// Checks if boat collection has any sunk boats and makes them visible
        /// </summary>
        /// <param name="boats"> Boat colletion </param>
        public void CheckForSunkBoats(IEnumerable<IBoat> boats);
    }
}
