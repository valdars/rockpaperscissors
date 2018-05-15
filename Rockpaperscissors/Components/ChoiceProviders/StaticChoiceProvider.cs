using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.ChoiceProviders
{
    public class StaticChoiceProvider : IChoiceProvider
    {
        private List<Choice> choices = new List<Choice>();

        public StaticChoiceProvider()
        {
            choices.Add(new Choice
            {
                id = "rock",
                title = "Rock",
                defeats = new List<string>()
                {
                    "scissors"
                }
            });

            choices.Add(new Choice
            {
                id = "paper",
                title = "Paper",
                defeats = new List<string>()
                {
                    "rock"
                }
            });

            choices.Add(new Choice
            {
                id = "scissors",
                title = "Scissors",
                defeats = new List<string>()
                {
                    "paper"
                }
            });
        }

        public Task<IEnumerable<Choice>> GetAll()
        {
            return Task.FromResult<IEnumerable<Choice>>(choices);
        }
    }
}
