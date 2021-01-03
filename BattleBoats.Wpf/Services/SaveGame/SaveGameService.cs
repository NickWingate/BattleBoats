using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace BattleBoats.Wpf.Services.SaveGame
{
    public class SaveGameService : ISaveGameService
    {
        public void SaveGame(GameModel game, string destinationFilePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreReadOnlyProperties = true
            };
            string gameString = JsonSerializer.Serialize(game, options);
            using (StreamWriter sw = new StreamWriter(@destinationFilePath))
            {
                sw.Write(gameString);
            }
        }
    }
}
