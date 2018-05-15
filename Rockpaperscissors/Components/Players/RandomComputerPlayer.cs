using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Players
{
    public class RandomComputerPlayer : IPlayer
    {
        public Task<Choice> GetChoice(Player player, GameState gameState, IEnumerable<Choice> choices)
        {
            var choice = PlayerHelper.GetRandomChoice(choices);
            return Task.FromResult<Choice>(choice);
        }
    }
}
