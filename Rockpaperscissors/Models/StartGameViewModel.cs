using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Models
{
    public class StartGameViewModel
    {
        public IEnumerable<Tuple<object, string>> PlayerChoices { get; set; }
        public IEnumerable<SelectListItem> PlayerSelectItems { set; get; }
    }
}
