using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.Models
{
    public struct Choice
    {
        public string id;
        public string title;
        public IEnumerable<string> defeats;

    }
}
