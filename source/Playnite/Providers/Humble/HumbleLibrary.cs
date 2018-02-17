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
            var games = new List<IGame>();




            return games;
        }
    }
    
}
