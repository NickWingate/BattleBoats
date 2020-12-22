namespace BattleBoats.Wpf.Models
{
    public interface IGameItem
    {
        int Column { get; set; }
        int ColumnSpan { get; }
        int Row { get; set; }
        int RowSpan { get; }
        bool IsSelected { get; set; }
        public bool Rotated { get; }
        public bool ShowItem { get; set; }

        Coordinate StartCoord { get; }
        Coordinate EndCoord { get; }
        CoordinateRange CoordinateRange { get; }

        void Rotate();
    }
}