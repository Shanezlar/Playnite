using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;
using NLog;
using Playnite.Models;
using Playnite.Providers.Humble;
using SteamKit2;
using Playnite.Services;
using Playnite.Database;
using System.Windows;
using System.Globalization;

namespace Playnite.Providers.Humble
{
    public class HumbleLibrary : IHumbleLibrary
    {
        private Logger logger = LogManager.GetCurrentClassLogger();



        public List<IGame> GetInstalledGames()
        {
            var games = new List<IGame>();

            return games;
        }

        public List<IGame> GetLibraryGames()
        {
            using (var api = new WebApiClient())
            {
                if (api.GetLoginRequired())
                {
                    throw new Exception("User is not logged in.");
                }

                var games = new List<IGame>();

                var page = api.GetOwnedGames();
                                
                List<GetOrdersResult> orders = JsonConvert.DeserializeObject<List<GetOrdersResult>>(page);
                
                foreach (var order in orders)
                {

                    var orderinfoResult = api.GetOrderInfo(order.gamekey);
                    
                    GetOrderInfoResult orderInfo = JsonConvert.DeserializeObject<GetOrderInfoResult>(orderinfoResult);
                    if (orderInfo == null) continue;
                    
                    foreach (var subproduct in orderInfo.subproducts)
                    {
                        if (subproduct.human_name == null) continue;
                        string human_name = subproduct.human_name;
                        foreach (var download in subproduct.downloads)
                        {
                            if (download.platform == null) continue;
                            if (download.platform == "windows")
                            {
                                games.Add(CreateGameForDownloadObject(download, subproduct));                        
                            }
                        }
                    }                    
                }

                return games;
            }
        }

        private Game CreateGameForDownloadObject(Download download, Subproduct subproduct)
        {
            var game = new Game()
            {
                Provider = Provider.Humble,
                ProviderId = download.machine_name,
                Name = subproduct.human_name,
                Icon = subproduct.icon,
                IsoPath = download.download_struct[0].url.web,
                InstallDirectory = @"",
                PlayTask = null,
                Description = @""                
            };
            return game;
        }
    }    
}
