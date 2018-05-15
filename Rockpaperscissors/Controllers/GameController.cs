using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rockpaperscissors.Components;
using Rockpaperscissors.Components.ChoiceProviders;
using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.GameStateStorages;
using Rockpaperscissors.Components.PlayerProviders;
using Rockpaperscissors.Components.Players;
using Rockpaperscissors.Models;

namespace Rockpaperscissors.Controllers
{
    public class GameController : Controller
    {
        private IPlayerProvider _playerProvider;
        private IGameStateStorage _gameStateStorage;
        private IChoiceProvider _choiceProvider;

        public GameController(IPlayerProvider playerProvider, IGameStateStorage gameStateStorage, IChoiceProvider choiceProvider)
        {
            _playerProvider = playerProvider;
            _gameStateStorage = gameStateStorage;
            _choiceProvider = choiceProvider;
        }

        public async Task<IActionResult> Index()
        {
            var playerChoices = await _playerProvider.GetList();
            var model = new StartGameViewModel()
            {
                PlayerChoices = playerChoices,
                PlayerSelectItems = playerChoices.Select(x => new SelectListItem()
                {
                    Value = x.Item1.ToString(),
                    Text = x.Item2
                })
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult StartGame(string player1, string player2)
        {
            var game = Game.StartNewGame(player1, player2);
            _gameStateStorage.Save(game.GameState);
            
            return new RedirectToActionResult("Round", null, new { id = game.GameState.Id });
        }

        public async Task<IActionResult> Round(string id)
        {
            var game = GetGame(id);
            var choices = await _choiceProvider.GetAll();

            var player = game.GetUnchocenPlayer();
            while (player != Player.None)
            {
                var playerId = game.GetPlayerId(player);
                var playerInstance = await _playerProvider.GetPlayer(playerId);
                var viewablePlayerInstance = playerInstance as IViewablePlayer;
                if (viewablePlayerInstance != null)
                {
                    var view = await viewablePlayerInstance.View(game.GameState, choices, PlayerHelper.CreateRedirectUrl(this.Url, game.GameState, player), player);
                    return view;
                }
                var choice = await playerInstance.GetChoice(player, game.GameState, choices);
                game.SetChoice(player, choice);
                _gameStateStorage.Save(game.GameState);
                player = game.GetUnchocenPlayer();
            }

            var winner = game.CalculateRoundWinner(choices);
            _gameStateStorage.Save(game.GameState);

            var round = game.GetCurrentRound();
            var model = new RoundEndViewModel()
            {
                Choices = choices,
                GameState = game.GameState,
                Round = round,
                RoundNo = game.GameState.Rounds.Count,
                Winner = winner
            };
            return View("RoundEnd", model);
        }

        public IActionResult NewRound(string id)
        {
            var game = GetGame(id);

            if(game.IsGameEnd())
            {
                return new RedirectToActionResult("GameEnd", null, new { id });
            }

            game.StartNewRound();
            _gameStateStorage.Save(game.GameState);

            return new RedirectToActionResult("Round", null, new { id });
        }

        public async Task<IActionResult> GameEnd(string id)
        {
            var game = GetGame(id);

            var winner = game.CalculateGameWinner();

            var model = new GameEndViewModel()
            {
                Choices = await _choiceProvider.GetAll(),
                GameState = game.GameState,
                Winner = winner
            };

            return View("GameEnd", model);
        }

        public async Task<IActionResult> Choose(string id, Player player)
        {
            var game = GetGame(id);
            var choices = await _choiceProvider.GetAll();
            var playerId = game.GetPlayerId(player);

            var playerInstance = await _playerProvider.GetPlayer(playerId);
            var viewablePlayerInstace = playerInstance as IViewablePlayer;
            
            await viewablePlayerInstace.SetContext(HttpContext);
            if (!await viewablePlayerInstace.Validate(game.GameState, choices, player))
            {
                return await viewablePlayerInstace.View(game.GameState, choices, PlayerHelper.CreateRedirectUrl(this.Url, game.GameState, player), player);
            }
            var choice = await playerInstance.GetChoice(player, game.GameState, choices);
            game.SetChoice(player, choice);
            _gameStateStorage.Save(game.GameState);

            return new RedirectToActionResult("Round", null, new { id = game.GameState.Id });
        }

        private Game GetGame(object id)
        {
            var gameState = _gameStateStorage.Retrieve(id);
            return Game.LoadGame(gameState);
        }
    }
}
