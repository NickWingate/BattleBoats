namespace BattleBoats.Wpf.Models
{
    public interface IGameItem
    {
        Coordinate Location { get; set; }
        int ColumnSpan { get; }
        int RowSpan { get; }
        bool IsSelected { get; set; }
        public bool Rotated { get; }
        public bool ShowItem { get; set; }

        CoordinateRange CoordinateRange { get; }

        void Rotate();
    }
}