using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Players
{
    public class TacticalComputerPlayer : IPlayer
    {
        public Task<Choice> GetChoice(Player player, GameState gameState, IEnumerable<Choice> choices)
        {
            var count = gameState.Rounds.Count;
            if (count == 1)
            {
                return Task.FromResult<Choice>(PlayerHelper.GetRandomChoice(choices));
            }
            var lastRound = gameState.Rounds.ElementAt(count - 2);
            var lastChoice = lastRound.Choices[(int)player];
            var choice = choices.First(x => x.defeats.Contains(lastChoice));
            return Task.FromResult<Choice>(choice);
        }
    }
}
