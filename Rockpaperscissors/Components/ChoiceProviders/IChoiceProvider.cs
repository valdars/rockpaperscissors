using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.ChoiceProviders
{
    public interface IChoiceProvider
    {
        Task<IEnumerable<Choice>> GetAll();
    }
}
