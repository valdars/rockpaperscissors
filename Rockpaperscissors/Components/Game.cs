using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components
{
    public class Game
    {
        const int maxRounds = 3;

        public GameState GameState { get; private set; }

        private Game(GameState gameState)
        {
            GameState = gameState;
        }

        public Player GetUnchocenPlayer()
        {
            var round = GetCurrentRound();
            if (!round.Choices.ContainsKey(1))
            {
                return Player.Player1;
            }
            else if (!round.Choices.ContainsKey(2))
            {
                return Player.Player2;
            }
            return Player.None;
        }

        public object GetPlayerId(Player player)
        {
            return GameState.Players[(int)player];
        }

        public Round GetCurrentRound()
        {
            return GameState.Rounds.Last();
        }

        public bool IsGameEnd()
        {
            return GameState.Rounds.Count == maxRounds;
        }

        public void SetChoice(Player player, Choice choice)
        {
            var round = GetCurrentRound();
            round.Choices[(int)player] = choice.id;
        }

        public int CalculateRoundWinner(IEnumerable<Choice> choices)
        {
            var round = GetCurrentRound();
            if (round.Choices[1] == round.Choices[2])
            {
                round.Winner = 0;
            }
            else
            {
                var choice1 = choices.First(x => x.id == round.Choices[1]);
                if (choice1.defeats.Contains(round.Choices[2]))
                {
                    round.Winner = 1;
                }
                else
                {
                    round.Winner = 2;
                }
            }
            return round.Winner;
        }

        public int CalculateGameWinner()
        {
            var count1 = GameState.Rounds.Count(x => x.Winner == 1);
            var count2 = GameState.Rounds.Count(x => x.Winner == 2);
            if (count1 == count2)
            {
                return 0;
            }
            else if (count1 > count2)
            {
                return 1;
            }
            return 2;
        }

        public void StartNewRound()
        {
            GameState.Rounds.Add(new Round());
        }

        public static Game StartNewGame(object player1, object player2)
        {
            var gameState = new GameState()
            {
                Id = Guid.NewGuid(),
                Players = new Dictionary<int, object>()
                {
                    {1, player1 },
                    {2, player2 }
                },
                Rounds = new List<Round>() { new Round() }
            };

            return new Game(gameState);
        }

        public static Game LoadGame(GameState gameState)
        {
            return new Game(gameState);
        }
    }
}
