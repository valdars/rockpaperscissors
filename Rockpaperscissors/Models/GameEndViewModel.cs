using Rockpaperscissors.Components;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Models
{
    public class GameEndViewModel
    {
        public int Winner { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
        public GameState GameState { get; set; }
    }
}
