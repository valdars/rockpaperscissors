using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Models
{
    public class GameState
    {
        public object Id { get; set; }

        public Dictionary<int, object> Players { get; set; }

        public List<Round> Rounds { get; set; }
    }
}
