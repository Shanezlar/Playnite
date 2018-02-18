using Playnite.MetaProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playnite.Models;

namespace Playnite.Providers.Humble
{
    public class HumbleMetadataProvider : IMetadataProvider
    {
        public GameMetadata GetGameData(string gameId)
        {
            /*var gameData = new Game("HumbleGame")
            {
                Provider = Provider.Steam,
                ProviderId = gameId
            };

            var steamLib = new HumbleLibrary();
            var data = steamLib.UpdateGameWithMetadata(gameData);*/
            return new GameMetadata();
        }

        public bool GetSupportsIdSearch()
        {
            return true;
        }

        public List<MetadataSearchResult> SearchGames(string gameName)
        {
            throw new NotSupportedException();
        }
    }
}
