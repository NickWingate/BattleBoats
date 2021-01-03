using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.SaveGame
{
    public interface ISaveGameService
    {
        public void SaveGame(GameModel game, string destinationFilePath);
    }
}
