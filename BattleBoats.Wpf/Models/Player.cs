using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class Player : ObservableObject
    {
        public Player(string name)
        {
            Name = name;
        }

        private ObservableCollection<IBoat> _boats = new ObservableCollection<IBoat>();
        public ObservableCollection<IBoat> Boats
        {
            get { return _boats; }
            set 
            { 
                _boats = value;
                OnPropertyChanged(nameof(Boats));
            }
        }

        private ObservableCollection<IGameItem> _hitmarkers = new ObservableCollection<IGameItem>();
        public ObservableCollection<IGameItem> HitMarkers
        {
            get { return _hitmarkers; }
            set 
            {
                _hitmarkers = value;
                OnPropertyChanged(nameof(HitMarkers));
            }
        }





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
