using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockpaperscissors.Components.Enums;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Players
{
    public interface IViewablePlayer
    {
        Task SetContext(HttpContext httpContext);
        Task<IActionResult> View(GameState gameState, IEnumerable<Choice> choices, string redirectUrl, Player player);
        Task<bool> Validate(GameState gameState, IEnumerable<Choice> choices, Player player);
    }
}
