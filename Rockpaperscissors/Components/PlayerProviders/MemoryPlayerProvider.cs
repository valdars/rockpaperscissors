using Rockpaperscissors.Components.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rockpaperscissors.Components.PlayerProviders
{
    public class MemoryPlayerProvider : IPlayerProvider
    {
        private static List<Tuple<object, string>> entries = new List<Tuple<object, string>>();
        private static Dictionary<object, Type> types = new Dictionary<object, Type>();

        public Task<IPlayer> GetPlayer(object id)
        {
            if(!types.ContainsKey(id))
            {
                throw new Exception("Player not found");
            }
            var type = types[id];
            var instance = (IPlayer)Activator.CreateInstance(type);
            return Task.FromResult<IPlayer>(instance);
        }

        public Task<IEnumerable<Tuple<object, string>>> GetList()
        {
            return Task.FromResult<IEnumerable<Tuple<object, string>>>(entries);
        }

        public static void RegisterPlayer<T>(object id, string title) where T : IPlayer
        {
            entries.Add(new Tuple<object, string>(id, title));
            types.Add(id, typeof(T));
        }
    }
}
