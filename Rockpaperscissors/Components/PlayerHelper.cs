using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components
{
    public static class PlayerHelper
    {
        public class ChoiceSelectViewModel
        {
            public IEnumerable<Choice> Choices { get; set; }
            public string RedirectUrl { get; set; }
            public int RoundNo { get; set; }
            public Player Player { get; set; }
            public bool IsError { get; set; }
        }

        public static string CreateRedirectUrl(IUrlHelper urlHelper, GameState gameState, Player player)
        {
            return urlHelper.Action("choose", "game", new { id = gameState.Id, player = (int)player });
        }

        public static ViewResult GetChoiceSelectView(GameState gameState, IEnumerable<Choice> choices, string redirectUrl, Player player, bool isError)
        {
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            viewData.Model = new ChoiceSelectViewModel
            {
                Choices = choices,
                RedirectUrl = redirectUrl,
                RoundNo = gameState.Rounds.Count,
                Player = player,
                IsError = isError
            };

            return new ViewResult()
            {
                ViewName = "ChoiceSelect",
                ViewData = viewData
            };
        }

        public static bool ValidateView(GameState gameState, IEnumerable<Choice> choices, HttpContext httpContext)
        {
            return httpContext.Request.Form.ContainsKey("choice");
        }

        public static Choice GetChoiceFromView(GameState gameState, IEnumerable<Choice> choices, HttpContext httpContext)
        {
            return choices.FirstOrDefault(x => x.id == httpContext.Request.Form["choice"]);
        }

        public static Choice GetRandomChoice(IEnumerable<Choice> choices)
        {
            var rand = new Random();
            var index = rand.Next(choices.Count());
            return choices.ElementAt(index);
        }
    }
}
