namespace BattleBoats.Wpf.Models
{
    public interface IGameItem
    {
        int Column { get; set; }
        int Row { get; set; }
        public bool ShowItem { get; set; }

        // Dont really need Location and EndCoord
        Coordinate Location { get; }
    }
}