using BattleBoats.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleBoats.Wpf.Services.BoatPlacement
{
    public interface IBoatPlacementGenerationService
    {
        List<IBoat> GenerateBoats(int quantity, int maxDimention);
    }
}
