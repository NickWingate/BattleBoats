using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class GameModel
    {
        public GameModel(Player user, Player computer)
        {
            User = user;
            Computer = computer;
        }
        /// <summary>
        /// Paramaterless ctor only for json deseralization
        /// </summary>
        public GameModel()
        {

        }

        public Player User { get; set; }
        public Player Computer { get; set; }
    }
}
