using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace BattleBoats.Wpf.Services.SaveGame
{
    public class SaveGameService : ISaveGameService
    {
        public void SaveGame(GameModel game, string destinationFilePath)
        {
            string gameString = JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            using (StreamWriter sw = new StreamWriter(@destinationFilePath))
            {
                sw.Write(gameString);
            }
        }
    }
}
