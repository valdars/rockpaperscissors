using Rockpaperscissors.Components;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Models
{
    public class RoundEndViewModel
    {
        public int RoundNo { get; set; }
        public int Winner { get; set; }
        public Round Round { get; set; }
        public IEnumerable<Choice> Choices { get; set; }
        public GameState GameState { get; set; }
    }
}
