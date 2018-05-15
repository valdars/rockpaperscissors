using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Rockpaperscissors.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.GameStateStorages
{
    public class SessionGameStateStorage : IGameStateStorage
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SessionGameStateStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public GameState Retrieve(object id)
        {
            var storeId = String.Format("GameState_{0}", id);
            var storeValue = _httpContextAccessor.HttpContext.Session.GetString(storeId);
            if(storeValue == null)
            {
                throw new Exception(String.Format("Gamestate with id {0} not found", id));
            }
            return JsonConvert.DeserializeObject<GameState>(storeValue);
        }

        public void Save(GameState state)
        {
            var storeValue = JsonConvert.SerializeObject(state);
            var storeId = String.Format("GameState_{0}", state.Id);
            _httpContextAccessor.HttpContext.Session.SetString(storeId, storeValue);
        }
    }
}
