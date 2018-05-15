using Rockpaperscissors.Components.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.PlayerProviders
{
    public interface IPlayerProvider
    {
        Task<IEnumerable<Tuple<object, string>>> GetList();
        Task<IPlayer> GetPlayer(object id);
    }
}
