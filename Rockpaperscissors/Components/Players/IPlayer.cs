using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Players
{
    public interface IPlayer
    {
        Task<Choice> GetChoice(Player player, GameState gameState, IEnumerable<Choice> choices);

    }
}
