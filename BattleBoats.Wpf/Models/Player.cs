using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;

namespace BattleBoats.Wpf.Models
{
    public class Player : ObservableObject
    {
        public Player(string name)
        {
            Name = name;
        }

        public ObservableCollection<IBoat> Boats { get; set; } = new ObservableCollection<IBoat>();
        public ObservableCollection<IGameItem> HitMarkers { get; set; } = new ObservableCollection<IGameItem>();

        [JsonIgnore]
        public TileState[,] GameBoard { get; set; }
        public int Health
        {
            get
            {
                int total = 0;
                foreach (IBoat boat in Boats)
                {
                    total += boat.Health;
                }
                return total;
            }
        }
        public string Name { get; set; }
    }
}
