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

        void Rotate();
    }
}