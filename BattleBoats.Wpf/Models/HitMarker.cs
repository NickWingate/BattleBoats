using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Models
{
    public class HitMarker : IGameItem
    {
        public HitMarker(int column, int row, TileState tileState)
        {
            Column = column;
            Row = row;
            TileState = tileState;
            ShowItem = true;
        }
        public HitMarker(Coordinate location, TileState tileState)
        {
            Column = location.XCoord;
            Row = location.YCoord;
            TileState = tileState;
            ShowItem = true;
        }
        public HitMarker()
        {
        }

        public int MaxGridDimention { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool ShowItem { get; set; }

        private TileState _tileState;
        public TileState TileState 
        {
            get { return _tileState; }
            set
            {
                if (value != TileState.Hit && value != TileState.Miss)
                {
                    throw new Exception($"HitMarker.{nameof(TileState)}must be either TileState.Hit or TileState.Miss, not {value}");
                }
                else
                {
                    _tileState = value;
                }
            } 
        }

        public Coordinate Location => new Coordinate(Column, Row);
    }
}
