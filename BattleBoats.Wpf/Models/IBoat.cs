namespace BattleBoats.Wpf.Models
{
    public interface IBoat
    {
        int Column { get; set; }
        int ColumnSpan { get; }
        int Length { get; set; }
        int Row { get; set; }
        int RowSpan { get; }
        int Health { get; set; }
        bool IsSelected { get; set; }

        Coordinate StartCoord { get; }
        Coordinate EndCoord { get; }
        CoordinateRange CoordinateRange { get; }

        void Rotate();
    }
}