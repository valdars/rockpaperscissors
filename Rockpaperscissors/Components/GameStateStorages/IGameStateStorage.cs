using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.GameStateStorages
{
    public interface IGameStateStorage
    {
        void Save(GameState state);

        GameState Retrieve(object id);
    }
}
