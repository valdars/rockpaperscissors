using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;

namespace Rockpaperscissors.Components.Players
{
    public class HumanPlayer : IPlayer, IViewablePlayer
    {
        private HttpContext _httpContext;
        private bool isViewError = false;

        public Task<Choice> GetChoice(Player player, GameState gameState, IEnumerable<Choice> choices)
        {
            return Task.FromResult<Choice>(PlayerHelper.GetChoiceFromView(gameState, choices, _httpContext));
        }

        public Task SetContext(HttpContext httpContext)
        {
            _httpContext = httpContext;
            return Task.FromResult(0);
        }

        public Task<bool> Validate(GameState gameState, IEnumerable<Choice> choices, Player player)
        {
            isViewError = !PlayerHelper.ValidateView(gameState, choices, _httpContext);
            return Task.FromResult<bool>(!isViewError);
        }

        public Task<IActionResult> View(GameState gameState, IEnumerable<Choice> choices, string redirectUrl, Player player)
        {
            var view = PlayerHelper.GetChoiceSelectView(gameState, choices, redirectUrl, player, isViewError);
            return Task.FromResult<IActionResult>(view);
        }
    }
}
