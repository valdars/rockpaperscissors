using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Models
{
    public class Round
    {
        public Dictionary<int, string> Choices { get; set; }
        public int Winner { get; set; }

        public Round()
        {
            Choices = new Dictionary<int, string>();
        }
    }
}
